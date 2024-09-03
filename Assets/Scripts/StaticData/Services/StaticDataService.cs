using System.Collections.Generic;
using System.Linq;
using Constants;
using StaticData.Data;
using UnityEngine;


namespace StaticData.Services
{
    public class StaticDataService
    {
        private Dictionary<int, LevelConfig> levelConfigs = new Dictionary<int, LevelConfig>();
        private Dictionary<ShapeID, ShapeConfig> shapeConfigs = new Dictionary<ShapeID, ShapeConfig>();


        public void Init()
        {
            LoadStaticData();
        }


        private void LoadStaticData()
        {
            LoadLevelsData();
            LoadShapesData();
        }


        private void LoadLevelsData()
        {
            levelConfigs = Resources.Load<LevelsStaticData>(RuntimeConstants.StaticDataPaths.LEVELS_STATIC_DATA)
                .levelConfigs.ToDictionary((x) => x.levelIndex, (x) => x);
        }


        private void LoadShapesData()
        {
            shapeConfigs = Resources.Load<ShapesStaticData>(RuntimeConstants.StaticDataPaths.SHAPES_STATIC_DATA)
                .shapeConfigs.ToDictionary((x) => x.shapeID, (x) => x);
        }


        public LevelConfig ForLevelIndex(int level)
        {
            return levelConfigs.GetValueOrDefault(level);
        }


        public ShapeConfig ForShapeID(ShapeID shapeID)
        {
            return shapeConfigs.GetValueOrDefault(shapeID);
        }
    }
}
