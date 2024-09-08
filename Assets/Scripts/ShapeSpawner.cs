using System.Collections.Generic;
using Factories;
using JoinPointComponents;
using ShapeComponents;
using StaticData.Data;
using StaticData.Services;
using UnityEngine;


public class ShapeSpawner
{
    private readonly JoinPointsSpawner joinPointsSpawner;
    private readonly StaticDataService staticDataService;
    private int pointsCounter = 0;


    public ShapeSpawner(JoinPointsSpawner joinPointsSpawner, StaticDataService staticDataService)
    {
        this.joinPointsSpawner = joinPointsSpawner;
        this.staticDataService = staticDataService;
    }



    public Shape SpawnShape(ShapeID shapeID)
    {
        Vector2 position = joinPointsSpawner.SpawnedPoints[pointsCounter].transform.position;
        Shape prefab = staticDataService.ForShapeID(shapeID).shapePrefab;

        Shape spawnedShape = Object.Instantiate(prefab, position, Quaternion.identity);
        pointsCounter++;
        return spawnedShape;
    }


    public void ResetCounter()
    {
        pointsCounter = 0;
    }
}
