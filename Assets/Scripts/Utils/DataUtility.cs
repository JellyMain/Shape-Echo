namespace Utils
{
    public static class DataUtility
    {
        public static int SecondsToMilliseconds(this float seconds)
        {
            return (int)(seconds * 1000);
        }
    }
}