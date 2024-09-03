using System.Collections.Generic;
using UnityEngine;


namespace StaticData.Data
{
    [CreateAssetMenu(fileName = "LevelsStaticData", menuName = "StaticData/LevelStaticData")]
    public class LevelsStaticData: ScriptableObject
    {
        public List<LevelConfig> levelConfigs;
    }
}
