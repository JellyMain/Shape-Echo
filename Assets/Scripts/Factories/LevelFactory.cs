using System.Collections.Generic;
using System.Threading.Tasks;
using Constants;
using Cysharp.Threading.Tasks;
using JoinPointComponents;
using ShapeComponents;
using StaticData.Data;
using StaticData.Services;
using UnityEngine;


namespace Factories
{
    public class LevelFactory
    {
        private readonly StaticDataService staticDataService;
        private readonly ShapeSpawner shapeSpawner;
        private readonly JoinPointsSpawner joinPointsSpawner;
        private float spawnBoxWidth;
        private JoinPoint joinPointPrefab;
        private readonly List<Shape> spawnedShapes = new List<Shape>();


        public LevelFactory(StaticDataService staticDataService, ShapeSpawner shapeSpawner,
            JoinPointsSpawner joinPointsSpawner)
        {
            this.staticDataService = staticDataService;
            this.shapeSpawner = shapeSpawner;
            this.joinPointsSpawner = joinPointsSpawner;
        }
        

        public async UniTaskVoid CreateLevel(int level)
        {
            LevelConfig currentLevelConfig = staticDataService.ForLevelIndex(level);
            spawnBoxWidth = currentLevelConfig.spawnBoxWidth;

            SpawnPoints(currentLevelConfig).Forget();

            int delay = DataUtility.SecondsToMilliseconds(staticDataService.AnimationsStaticData
                .pointsAndShapesSpawnDifference);
            await UniTask.Delay(delay);

            await SpawnShapes(currentLevelConfig);


            DestroyShapes().Forget();
        }


        private async UniTaskVoid SpawnPoints(LevelConfig currentLevelConfig)
        {
            int joinPoints = currentLevelConfig.joinPointShapes.Count;
            float equalDistance = spawnBoxWidth / (joinPoints - 1);

            for (int i = 0; i < joinPoints; i++)
            {
                Vector2 position = new Vector2(i * equalDistance - spawnBoxWidth / 2, 0);
                joinPointsSpawner.SpawnJoinPoint(position);

                int delay = DataUtility.SecondsToMilliseconds(staticDataService.AnimationsStaticData.pointsSpawnDelay);
                await UniTask.Delay(delay);
            }
        }


        private async UniTask SpawnShapes(LevelConfig currentLevelConfig)
        {
            for (int i = 0; i < currentLevelConfig.joinPointShapes.Count; i++)
            {
                ShapeID shapeID = currentLevelConfig.joinPointShapes[i];

                Shape spawnedShape = shapeSpawner.SpawnShape(shapeID);

                spawnedShapes.Add(spawnedShape);

                joinPointsSpawner.SpawnedPoints[i].PointAnimator.OnShapeAttracting();

                int delay = DataUtility.SecondsToMilliseconds(staticDataService.AnimationsStaticData.shapesSpawnDelay);
                await UniTask.Delay(delay);
            }
            
            shapeSpawner.ResetCounter();
        }


        private async UniTaskVoid DestroyShapes()
        {
            for (int i = 0; i < spawnedShapes.Count; i++)
            {
                await spawnedShapes[i].Kill();
                joinPointsSpawner.SpawnedPoints[i].PointAnimator.OnShapeNotAttracting();
            }
        }


        public async void CleanLevel()
        {
            foreach (JoinPoint point in joinPointsSpawner.SpawnedPoints)
            {
                point.Kill();

                int delay = DataUtility.SecondsToMilliseconds(staticDataService.AnimationsStaticData.cleanLevelDelay);
                await Task.Delay(delay);
            }
        }
    }
}
