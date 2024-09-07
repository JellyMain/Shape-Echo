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
    }
}
