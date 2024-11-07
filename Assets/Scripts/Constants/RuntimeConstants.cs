namespace Constants
{
    public static class RuntimeConstants
    {
        public static class PrefabPaths
        {
            public const string PLAYER = "Prefabs/Player/Player";
            public const string HUD = "Prefabs/UI/Hud";
            public const string TEST_WALL = "Prefabs/DungeonParts/Walls/Wall";
        }


        public static class Scenes
        {
            public const string GAME_SCENE = "GameplayScene";
            public const string MAIN_MENU_SCENE = "MenuScene";
        }


        public static class StaticDataPaths
        {
            public const string DUNGEON_ROOMS_CONFIG = "StaticData/Dungeon/Rooms/DungeonRoomsConfig";
            public const string DUNGEON_STATIC_DATA = "StaticData/Dungeon/DungeonStaticData";
            public const string PLAYER_STATIC_DATA = "StaticData/Player/PlayerStaticData";
            public const string ENEMIES_STATIC_DATA = "StaticData/Enemies/TriangleEnemy";
            public const string BULLET_SLOTS_CONFIG = "StaticData/UI/BulletsUIConfig";
        }


        public static class Tags
        {
            public const string ENEMY_SPAWNER = "EnemySpawner";
            public const string PLAYER = "Player";
        }
    }
}
