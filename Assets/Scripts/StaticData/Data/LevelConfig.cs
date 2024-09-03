using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;


namespace StaticData.Data
{
    [Serializable]
    public class LevelConfig
    {
        public int levelIndex;
        public List<ShapeID> joinPointShapes;
        public float spawnBoxWidth = 10;
        [ShowIf("@joinPointShapes.Count % 2 == 0")]
        public float spawnPointsOffset = 0.7f;
    }
}
