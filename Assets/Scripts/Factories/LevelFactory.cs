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
        private float spawnBoxWidth = 10;
        private float pointOffset = 0.7f;
        private JoinPoint joinPointPrefab;
        private readonly List<JoinPoint> spawnedPoints = new List<JoinPoint>();
        private readonly List<Shape> spawnedShapes = new List<Shape>();


        public LevelFactory(StaticDataService staticDataService)
        {
            this.staticDataService = staticDataService;
        }


        public void InitPrefabs()
        {
            joinPointPrefab = Resources.Load<JoinPoint>(RuntimeConstants.PrefabPaths.JOIN_POINT);
        }


        public async UniTaskVoid CreateLevel(int level)
        {
            LevelConfig currentLevelConfig = staticDataService.ForLevelIndex(level);
            spawnBoxWidth = currentLevelConfig.spawnBoxWidth;
            pointOffset = currentLevelConfig.spawnPointsOffset;

            SpawnPoints(currentLevelConfig).Forget();
            await UniTask.Delay(1500);
            SpawnShapes(currentLevelConfig).Forget();
        }


        private async UniTaskVoid SpawnPoints(LevelConfig currentLevelConfig)
        {
            int joinPoints = currentLevelConfig.joinPointShapes.Count;
            float equalDistance = spawnBoxWidth / (joinPoints - 1);


            for (int point = 0; point < joinPoints; point++)
            {
                Vector2 position = new Vector2(point * equalDistance - spawnBoxWidth / 2, 0);
                InstantiateJoinPoint(position);

                await UniTask.Delay(250);
            }
        }


        private void InstantiateJoinPoint(Vector2 position)
        {
            JoinPoint joinPoint = Object.Instantiate(joinPointPrefab, position, Quaternion.identity);
            spawnedPoints.Add(joinPoint);
        }


        private async UniTaskVoid SpawnShapes(LevelConfig currentLevelConfig)
        {
            for (int i = 0; i < currentLevelConfig.joinPointShapes.Count; i++)
            {
                ShapeID shapeID = currentLevelConfig.joinPointShapes[i];
                Shape prefab = staticDataService.ForShapeID(shapeID).shapePrefab;
                Shape spawnedShape =
                    Object.Instantiate(prefab, spawnedPoints[i].transform.position, Quaternion.identity);
                spawnedShapes.Add(spawnedShape);

                await UniTask.Delay(500);
            }
        }


        private void DestroyShapes()
        {
            foreach (Shape shape in spawnedShapes)
            {
                
            }
        }


        private static bool IsEvenNumber(int joinPoints)
        {
            return joinPoints % 2 == 0;
        }


        public async void CleanLevel()
        {
            foreach (JoinPoint point in spawnedPoints)
            {
                point.Kill();
                await Task.Delay(100);
            }
        }
    }
}
