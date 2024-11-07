using System.Collections.Generic;
using UnityEngine;
using GeometryUtility = Utils.GeometryUtility;


namespace Dungeon
{
    public class DelaunayTriangulation
    {
        public List<(Vector2, Vector2, Vector2)> Triangles { get; private set; } =
            new List<(Vector2, Vector2, Vector2)>();

        private (Vector2, Vector2, Vector2) superTriangle;


        private void CreateSuperTriangle(int gridSize)
        {
            Vector2 pointA;
            Vector2 pointB;
            Vector2 pointC;

            do
            {
                pointA = new Vector2(0, gridSize * Random.Range(1, 10));
                pointB = new Vector2(gridSize * 10, -gridSize * 5);
                pointC = new Vector2(-gridSize * 11, -gridSize * 6);
            } while (GeometryUtility.AreTrianglePointsCollinear(pointA, pointB, pointC));

            superTriangle = (pointA, pointB, pointC);

            Triangles.Add(superTriangle);
        }



        public List<(RoomBase, RoomBase)> Triangulate(int gridSize, List<RoomBase> rooms)
        {
            List<(RoomBase, RoomBase)> edges = new List<(RoomBase, RoomBase)>();
            Dictionary<Vector2, RoomBase> positionToRoom = new Dictionary<Vector2, RoomBase>();

            foreach (var room in rooms)
            {
                positionToRoom[room.transform.position] = room;
            }

            CreateSuperTriangle(gridSize);

            foreach (RoomBase room in rooms)
            {
                List<(Vector2, Vector2, Vector2)> badTriangles = new List<(Vector2, Vector2, Vector2)>();


                foreach ((Vector2 pointA, Vector2 pointB, Vector2 pointC) in Triangles)
                {
                    if (GeometryUtility.IsInCircumcircle(room.transform.position, pointA, pointB, pointC))
                    {
                        badTriangles.Add((pointA, pointB, pointC));
                    }
                }


                foreach (var badTriangle in badTriangles)
                {
                    Triangles.Remove(badTriangle);
                }


                List<(Vector2, Vector2)> boundaryEdges = GetBoundaryEdges(badTriangles);

                foreach (var edge in boundaryEdges)
                {
                    Triangles.Add((edge.Item1, edge.Item2, room.transform.position));
                }
            }

            RemoveSuperTriangle();

            foreach ((Vector2 a, Vector2 b, Vector2 c) in Triangles)
            {
                if (positionToRoom.ContainsKey(a) && positionToRoom.ContainsKey(b))
                {
                    edges.Add((positionToRoom[a], positionToRoom[b]));
                }
                if (positionToRoom.ContainsKey(a) && positionToRoom.ContainsKey(c))
                {
                    edges.Add((positionToRoom[a], positionToRoom[c]));
                }
                if (positionToRoom.ContainsKey(b) && positionToRoom.ContainsKey(c))
                {
                    edges.Add((positionToRoom[b], positionToRoom[c]));
                }
            }

            return edges;
        }


        private void RemoveSuperTriangle()
        {
            Triangles.RemoveAll(triangle =>
                IsSuperTriangleVertex(triangle.Item1) ||
                IsSuperTriangleVertex(triangle.Item2) ||
                IsSuperTriangleVertex(triangle.Item3));
        }



        private List<(Vector2, Vector2)> GetBoundaryEdges(List<(Vector2, Vector2, Vector2)> badTriangles)
        {
            Dictionary<(Vector2, Vector2), int> edgeCount = new Dictionary<(Vector2, Vector2), int>();
            List<(Vector2, Vector2)> boundaryEdges = new List<(Vector2, Vector2)>();


            foreach ((Vector2 pointA, Vector2 pointB, Vector2 pointC) in badTriangles)
            {
                AddEdge(edgeCount, pointA, pointB);
                AddEdge(edgeCount, pointB, pointC);
                AddEdge(edgeCount, pointC, pointA);
            }


            foreach (var edge in edgeCount)
            {
                if (edge.Value == 1)
                {
                    boundaryEdges.Add(edge.Key);
                }
            }


            return boundaryEdges;
        }



        private void AddEdge(Dictionary<(Vector2, Vector2), int> edgeCount, Vector2 v1, Vector2 v2)
        {
            var edge = (v1, v2);

            if (v1.x > v2.x || (v1.x == v2.x && v1.y > v2.y))
            {
                edge = (v2, v1);
            }


            if (edgeCount.ContainsKey((edge)))
            {
                edgeCount[edge]++;
            }
            else
            {
                edgeCount[edge] = 1;
            }
        }


        private bool IsSuperTriangleVertex(Vector2 vertex)
        {
            (Vector2 superA, Vector2 superB, Vector2 superC) = superTriangle;

            return vertex == superA || vertex == superB || vertex == superC;
        }
    }
}
