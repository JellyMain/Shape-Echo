using System.Collections.Generic;
using System.Linq;
using ShapeComponents;
using StaticData.Data;
using StaticData.Services;
using UnityEngine;


public class LevelValidator
{
    private readonly StaticDataService staticDataService;
    private List<ShapeID> levelCorrectShapes;
    private List<ShapeID> levelCurrentShapes;


    public LevelValidator(StaticDataService staticDataService)
    {
        this.staticDataService = staticDataService;
    }

    
    public void Init()
    {
        levelCorrectShapes = staticDataService.ForLevelIndex(1).joinPointShapes;
        levelCurrentShapes = new List<ShapeID>(new ShapeID[levelCorrectShapes.Count]);
    }


    public void AddShape(ShapeID shapeID, int spawnIndex)
    {
        levelCurrentShapes[spawnIndex] = shapeID;
    }


    public void RemoveShape(ShapeID shapeID, int spawnIndex)
    {
        levelCurrentShapes[spawnIndex] = ShapeID.None;
    }


    public void ValidateLevel()
    {
        if (levelCorrectShapes.SequenceEqual(levelCurrentShapes))
        {
            Debug.Log("correct");
        }
        else
        {
            Debug.Log("not correct");
        }
    }
}
