using UnityEngine;


namespace Utils
{
    public static class DataUtility
    {
        public static int SecondsToMilliseconds(this float seconds)
        {
            return (int)(seconds * 1000);
        }


        public static Quaternion DirectionToQuaternion(Vector2 direction)
        {
            float targetAngle =  Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0, 0, targetAngle);
        }
    }
}