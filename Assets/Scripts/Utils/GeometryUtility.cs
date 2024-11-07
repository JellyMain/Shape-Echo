using UnityEngine;


namespace Utils
{
    public static class GeometryUtility
    {
        public static Vector2 GetCircumcenter(Vector2 pointA, Vector2 pointB, Vector2 pointC)
        {
            Vector2 abMid = new Vector2((pointA.x + pointB.x) / 2, (pointA.y + pointB.y) / 2);
            Vector2 bcMid = new Vector2((pointB.x + pointC.x) / 2, (pointB.y + pointC.y) / 2);

            float abSlope = (pointB.y - pointA.y) / (pointB.x - pointA.x);
            float bcSlope = (pointC.y - pointB.y) / (pointC.x - pointB.x);


            float abPerpendSlope = -1 / abSlope;
            float bcPerpendSlope = -1 / bcSlope;

            float c1 = abMid.y - abPerpendSlope * abMid.x;
            float c2 = bcMid.y - bcPerpendSlope * bcMid.x;

            float circumcenterX = (c2 - c1) / (abPerpendSlope - bcPerpendSlope);
            float circumcenterY = abPerpendSlope * circumcenterX + c1;

            return new Vector2(circumcenterX, circumcenterY);
        }


        public static bool IsInCircumcircle(Vector2 point, Vector2 pointA, Vector2 pointB, Vector2 pointC)
        {
            Vector2 circumcenter = GetCircumcenter(pointA, pointB, pointC);
            float radius = Vector2.Distance(circumcenter, pointA);

            return Vector2.Distance(circumcenter, point) <= radius;
        }


        public static bool AreTrianglePointsCollinear(Vector2 pointA, Vector2 pointB, Vector2 pointC)
        {
            float area = Mathf.Abs(pointA.x * (pointB.y - pointC.y) +
                                   pointB.x * (pointC.y - pointA.y) +
                                   pointC.x * (pointA.y - pointB.y)) / 2;

            return Mathf.Approximately(area, 0);
        }
    }
}
