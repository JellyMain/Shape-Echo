using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Dungeon
{
    public class MinimumSpanningTree
    {
        public List<(RoomBase, RoomBase)> ComputeMst(List<(Vector2, Vector2, Vector2)> triangles,
            List<RoomBase> rooms)
        {
            List<(RoomBase, RoomBase)> minimumSpanningTree = new List<(RoomBase, RoomBase)>();

            Dictionary<Vector2, RoomBase> positionToRoom = new Dictionary<Vector2, RoomBase>();

            foreach (var room in rooms)
            {
                positionToRoom[room.transform.position] = room;
            }

            List<(Vector2, Vector2, float)> edgesWithWeights = new List<(Vector2, Vector2, float)>();

            foreach ((Vector2 a, Vector2 b, Vector2 c) in triangles)
            {
                edgesWithWeights.Add((a, b, Vector2.Distance(a, b)));
                edgesWithWeights.Add((b, c, Vector2.Distance(b, c)));
                edgesWithWeights.Add((a, c, Vector2.Distance(a, c)));
            }

            edgesWithWeights = edgesWithWeights.OrderBy(edge => edge.Item3).ToList();


            Dictionary<Vector2, Vector2> roomsParents = new Dictionary<Vector2, Vector2>();

            foreach (RoomBase room in rooms)
            {
                roomsParents[room.transform.position] = room.transform.position;
            }


            foreach (var (vertex1, vertex2, weight) in edgesWithWeights)
            {
                if (Find(vertex1, roomsParents) != Find(vertex2, roomsParents))
                {
                    if (positionToRoom.ContainsKey(vertex1) && positionToRoom.ContainsKey(vertex2))
                    {
                        minimumSpanningTree.Add((positionToRoom[vertex1], positionToRoom[vertex2]));
                    }

                    Union(vertex1, vertex2, roomsParents);
                }
            }

            return minimumSpanningTree;
        }


        private Vector2 Find(Vector2 v, Dictionary<Vector2, Vector2> roomsParents)
        {
            if (roomsParents[v] != v)
            {
                roomsParents[v] = Find(roomsParents[v], roomsParents);
            }

            return roomsParents[v];
        }


        private void Union(Vector2 u, Vector2 v, Dictionary<Vector2, Vector2> roomsParents)
        {
            Vector2 rootU = Find(u, roomsParents);
            Vector2 rootV = Find(v, roomsParents);

            if (rootU != rootV)
            {
                roomsParents[rootU] = rootV;
            }
        }
    }
}
