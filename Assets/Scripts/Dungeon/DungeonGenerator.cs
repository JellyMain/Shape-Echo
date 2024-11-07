using System;
using System.Collections.Generic;
using Constants;
using Cysharp.Threading.Tasks;
using Pathfinding;
using StaticData.Services;
using UnityEngine;
using Object = UnityEngine.Object;
using Path = Pathfinding.Path;
using Random = UnityEngine.Random;


namespace Dungeon
{
    public class DungeonGenerator
    {
        private readonly List<RoomBase> rooms = new List<RoomBase>();

        private readonly List<(RoomBase, DoorDirection, RoomBase, DoorDirection)> doorConnections =
            new List<(RoomBase, DoorDirection, RoomBase, DoorDirection)>();

        private List<(RoomBase, RoomBase)> allRoomConnections = new List<(RoomBase, RoomBase)>();
        private List<(RoomBase, RoomBase)> selectedRoomConnections = new List<(RoomBase, RoomBase)>();
        private readonly StaticDataService staticDataService;
        private readonly DelaunayTriangulation delaunayTriangulation = new DelaunayTriangulation();
        private readonly MinimumSpanningTree minimumSpanningTree = new MinimumSpanningTree();
        private bool[,] grid;
        private Dictionary<RoomType, int> roomTypesCount;
        private int gridSize;
        private float edgeAddChance;
        private int margin;
        private GameObject wallPrefab;
        private Seeker seeker;



        public DungeonGenerator(StaticDataService staticDataService)
        {
            this.staticDataService = staticDataService;
        }


        public void GenerateDungeon()
        {
            LoadWallPrefab();
            InitializeAstarPathfinding();
            LoadDungeonSettings();
            SpawnRooms();
            GenerateConnections();
            FindDoorConnections();
            FindPaths();
        }


        private void LoadWallPrefab()
        {
            wallPrefab = Resources.Load<GameObject>(RuntimeConstants.PrefabPaths.TEST_WALL);
        }



        private void InitializeAstarPathfinding()
        {
            GameObject pathfinder = new GameObject("Pathfinder");
            AstarPath astarPath = pathfinder.AddComponent<AstarPath>();
            seeker = pathfinder.AddComponent<Seeker>();
            SimpleSmoothModifier simpleSmooth = pathfinder.AddComponent<SimpleSmoothModifier>();

            simpleSmooth.smoothType = SimpleSmoothModifier.SmoothType.CurvedNonuniform;
            simpleSmooth.factor = staticDataService.DungeonStaticData.smoothingFactor;
            simpleSmooth.maxSegmentLength = staticDataService.DungeonStaticData.smoothingSegmentLength;

            AstarData data = astarPath.data;

            GridGraph gridGraph = data.AddGraph(typeof(GridGraph)) as GridGraph;
            gridGraph.is2D = true;
            gridGraph.SetDimensions(staticDataService.DungeonStaticData.gridSize,
                staticDataService.DungeonStaticData.gridSize, 1);
            gridGraph.neighbours = NumNeighbours.Four;
            gridGraph.collision.use2D = true;
            gridGraph.collision.type = ColliderType.Sphere;
            gridGraph.collision.mask = staticDataService.DungeonStaticData.obstacleLayer;
            gridGraph.initialPenalty = staticDataService.DungeonStaticData.initialPenalty;

            astarPath.heuristic = Heuristic.Manhattan;

            astarPath.Scan();
        }


        private void LoadDungeonSettings()
        {
            gridSize = staticDataService.DungeonStaticData.gridSize;
            edgeAddChance = staticDataService.DungeonStaticData.edgeAddChance;
            margin = staticDataService.DungeonStaticData.roomsMargin;
            roomTypesCount = staticDataService.DungeonStaticData.roomTypesCount;
        }


        private void SpawnRooms()
        {
            grid = new bool[gridSize, gridSize];
            Vector2 offset = new Vector2(gridSize / 2, gridSize / 2);
            
            
            foreach (var roomTypeCount in roomTypesCount)
            {
                RoomType roomType = roomTypeCount.Key;
                int requiredCount = roomTypeCount.Value;
                Dictionary<RoomName, RoomBase> roomsForType = staticDataService.RoomsForRoomType(roomType);

                for (int i = 0; i < requiredCount; i++)
                {
                    RoomBase randomRoom = roomsForType[(RoomName)Random.Range(0, roomsForType.Count)];
                    
                    bool isValid = false;

                    for (int attempt = 0; attempt < 1000; attempt++)
                    {
                        int x = Random.Range(randomRoom.WidthLeft, gridSize - randomRoom.WidthRight);
                        int y = Random.Range(randomRoom.HeightBottom, gridSize - randomRoom.HeightTop);

                        Vector2Int gridRoomPosition = new Vector2Int(x, y);
                        Vector2 worldRoomPosition = new Vector2(x, y) - offset;

                        if (HasNoOverlap(gridRoomPosition, randomRoom))
                        {
                            RoomBase spawnedRoom =
                                Object.Instantiate(randomRoom, worldRoomPosition, Quaternion.identity);
                            rooms.Add(spawnedRoom);
                            FillCells(gridRoomPosition, randomRoom);
                            isValid = true;
                            break;
                        }
                    }

                    if (!isValid)
                    {
                        Debug.LogError($"Couldn't find space for a {roomType} room after 1000 attempts.");
                    }
                }
            }
        }


        private void GenerateConnections()
        {
            allRoomConnections = delaunayTriangulation.Triangulate(gridSize, rooms);
            selectedRoomConnections =
                minimumSpanningTree.ComputeMst(delaunayTriangulation.Triangles, rooms);
            AddMoreConnections();
        }


        private void FillCells(Vector2Int roomPosition, RoomBase roomBase)
        {
            int startX = roomPosition.x - roomBase.WidthLeft - margin;
            int startY = roomPosition.y - roomBase.HeightBottom - margin;

            for (int x = startX; x < startX + roomBase.WidthLeft + roomBase.WidthRight + margin; x++)
            {
                for (int y = startY; y < startY + roomBase.HeightBottom + roomBase.HeightTop + margin; y++)
                {
                    grid[x, y] = true;
                }
            }
        }


        private bool HasNoOverlap(Vector2Int roomPosition, RoomBase roomBase)
        {
            int startX = roomPosition.x - roomBase.WidthLeft - margin;
            int startY = roomPosition.y - roomBase.HeightBottom - margin;

            for (int x = startX; x < startX + roomBase.WidthLeft + roomBase.WidthRight + margin; x++)
            {
                for (int y = startY; y < startY + roomBase.HeightBottom + roomBase.HeightTop + margin; y++)
                {
                    if (x < 0 || y < 0 || x >= gridSize || y >= gridSize || grid[x, y])
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        private void AddMoreConnections()
        {
            Dictionary<RoomBase, int> connectionCounter = new Dictionary<RoomBase, int>();

            foreach ((RoomBase roomA, RoomBase roomB) in allRoomConnections)
            {
                float randomNumber = Random.Range(0, 100);

                connectionCounter.TryAdd(roomA, 0);
                connectionCounter.TryAdd(roomB, 0);

                if (!selectedRoomConnections.Contains((roomA, roomB)) &&
                    !selectedRoomConnections.Contains((roomB, roomA)) &&
                    randomNumber <= edgeAddChance &&
                    connectionCounter[roomA] < 4 &&
                    connectionCounter[roomB] < 4)
                {
                    connectionCounter[roomA]++;
                    connectionCounter[roomB]++;
                    selectedRoomConnections.Add((roomA, roomB));
                }
            }
        }



        private void FindDoorConnections()
        {
            Dictionary<RoomBase, List<DoorDirection>> doorPositions =
                new Dictionary<RoomBase, List<DoorDirection>>();

            foreach ((RoomBase roomA, RoomBase roomB) in selectedRoomConnections)
            {
                Vector2 direction = roomB.transform.position - roomA.transform.position;
                DoorDirection doorPosition;

                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    doorPosition = direction.x > 0 ? DoorDirection.Right : DoorDirection.Left;
                }
                else
                {
                    doorPosition = direction.y > 0 ? DoorDirection.Top : DoorDirection.Bottom;
                }

                if (!doorPositions.ContainsKey(roomA))
                {
                    doorPositions[roomA] = new List<DoorDirection>();
                }

                if (!doorPositions[roomA].Contains(doorPosition))
                {
                    doorPositions[roomA].Add(doorPosition);
                }


                DoorDirection oppositeDoorPosition = doorPosition switch
                {
                    DoorDirection.Right => DoorDirection.Left,
                    DoorDirection.Left => DoorDirection.Right,
                    DoorDirection.Top => DoorDirection.Bottom,
                    DoorDirection.Bottom => DoorDirection.Top,
                    _ => doorPosition
                };

                if (!doorPositions.ContainsKey(roomB))
                {
                    doorPositions[roomB] = new List<DoorDirection>();
                }

                if (!doorPositions[roomB].Contains(oppositeDoorPosition))
                {
                    doorPositions[roomB].Add(oppositeDoorPosition);
                }

                doorConnections.Add((roomA, doorPosition, roomB, oppositeDoorPosition));
            }


            CloseUnusedDoors(doorPositions);
        }


        private static void CloseUnusedDoors(Dictionary<RoomBase, List<DoorDirection>> doorPositions)
        {
            foreach (RoomBase room in doorPositions.Keys)
            {
                room.CloseDoors(doorPositions[room]);
            }
        }


        private async void FindPaths()
        {
            AstarPath.active.Scan();

            foreach ((RoomBase roomA, DoorDirection doorPositionA, RoomBase roomB, DoorDirection doorPositionB) in
                     doorConnections)
            {
                Vector2 doorA = roomA.DoorsDirections[doorPositionA].transform.position;
                Vector2 doorB = roomB.DoorsDirections[doorPositionB].transform.position;

                seeker.pathCallback += OnPathCompleted;

                Path path = seeker.StartPath(doorA, doorB);

                await path.WaitForPath().ToUniTask();
            }
        }



        private void OnPathCompleted(Path path)
        {
            PrioritizeUsedPaths(path);


            foreach (var node in path.path)
            {
                Object.Instantiate(wallPrefab, (Vector3)node.position, Quaternion.identity);
            }

            for (int index = 0; index < path.vectorPath.Count - 1; index++)
            {
                Vector3 point = path.vectorPath[index];
                Vector3 nextPoint = path.vectorPath[index + 1];
                Debug.DrawLine(point, nextPoint, Color.green, 1000);
            }
        }


        private static void PrioritizeUsedPaths(Path path)
        {
            foreach (GraphNode node in path.path)
            {
                node.Penalty = 0;
            }
        }
    }
}
