using System.Collections.Generic;
using Constants;
using JoinPointComponents;
using StaticData.Data;
using UnityEngine;


public class JoinPointsSpawner
{
    private JoinPoint joinPointPrefab;
    public List<JoinPoint> SpawnedPoints { get; private set; } = new List<JoinPoint>();


    public void Init()
    {
        joinPointPrefab = Resources.Load<JoinPoint>(RuntimeConstants.PrefabPaths.JOIN_POINT);
    }


    public void SpawnJoinPoint(Vector2 position)
    {
        JoinPoint spawnedPoint = Object.Instantiate(joinPointPrefab, position, Quaternion.identity);
        SpawnedPoints.Add(spawnedPoint);
    }
}
