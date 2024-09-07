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
        private readonly LevelValidator levelValidator;
        private float spawnBoxWidth;
        private JoinPoint joinPointPrefab;
        private readonly List<JoinPoint> spawnedPoints = new List<JoinPoint>();
        private readonly List<Shape> spawnedShapes = new List<Shape>();


        public LevelFactory(StaticDataService staticDataService, LevelValidator levelValidator)
        {
            this.staticDataService = staticDataService;
            this.levelValidator = levelValidator;
        }


        public void InitPrefabs()
        {
            joinPointPrefab = Resources.Load<JoinPoint>(RuntimeConstants.PrefabPaths.JOIN_POINT);
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
                InstantiateJoinPoint(position, i);

                int delay = DataUtility.SecondsToMilliseconds(staticDataService.AnimationsStaticData.pointsSpawnDelay);
                await UniTask.Delay(delay);
            }
        }


        private void InstantiateJoinPoint(Vector2 position, int spawnIndex)
        {
            JoinPoint joinPoint = Object.Instantiate(joinPointPrefab, position, Quaternion.identity);
            joinPoint.SpawnIndex = spawnIndex;
            spawnedPoints.Add(joinPoint);
        }


        private async UniTask SpawnShapes(LevelConfig currentLevelConfig)
        {
            for (int i = 0; i < currentLevelConfig.joinPointShapes.Count; i++)
            {
                ShapeID shapeID = currentLevelConfig.joinPointShapes[i];

                Shape prefab = staticDataService.ForShapeID(shapeID).shapePrefab;
                Shape spawnedShape =
                    Object.Instantiate(prefab, spawnedPoints[i].transform.position, Quaternion.identity);

                spawnedShape.DisableCollider();

                spawnedShapes.Add(spawnedShape);

                spawnedPoints[i].PointAnimator.OnShapeAttracting();

                int delay = DataUtility.SecondsToMilliseconds(staticDataService.AnimationsStaticData.shapesSpawnDelay);
                await UniTask.Delay(delay);
            }
        }


        private async UniTaskVoid DestroyShapes()
        {
            for (int i = 0; i < spawnedShapes.Count; i++)
            {
                await spawnedShapes[i].Kill();
                spawnedPoints[i].PointAnimator.OnShapeNotAttracting();
            }
        }


        public async void CleanLevel()
        {
            foreach (JoinPoint point in spawnedPoints)
            {
                point.Kill();
                
                int delay = DataUtility.SecondsToMilliseconds(staticDataService.AnimationsStaticData.cleanLevelDelay);
                await Task.Delay(delay);
            }
        }
    }
}
