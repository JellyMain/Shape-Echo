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
        private float pointOffset;
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
            pointOffset = currentLevelConfig.spawnPointsOffset;

            SpawnPoints(currentLevelConfig).Forget();
            await UniTask.Delay(1500);
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

                await UniTask.Delay(100);
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
                
                spawnedShape.GetComponent<Collider2D>().enabled = false;
                
                spawnedShapes.Add(spawnedShape);
                
                spawnedPoints[i].GetComponent<JoinPointAnimator>().OnShapeAttracting();

                await UniTask.Delay(100);
            }
        }


        private async UniTaskVoid DestroyShapes()
        {
            for (int i = 0; i < spawnedShapes.Count; i++)
            {
                await spawnedShapes[i].Kill();
                spawnedPoints[i].GetComponent<JoinPointAnimator>().OnShapeNotAttracting();
            }
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
