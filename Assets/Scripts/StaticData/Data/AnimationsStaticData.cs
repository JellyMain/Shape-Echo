using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace StaticData.Data
{
    [CreateAssetMenu(fileName = "AnimationsStaticData", menuName = "StaticData/AnimationsStaticData")]
    public class AnimationsStaticData: ScriptableObject
    {
        [Title("Shapes Animations")]
        public float shapesSpawnDelay = 0.1f;
        public float shapeSpawnDuration = 1f;
        public float shapePulsingDuration = 0.3f;
        public float shapeStopPulsingDuration = 0.3f;
        public float shapeDestroyDuration = 0.5f;
        
        [Title("Points Animations")]
        public float pointsSpawnDelay = 0.1f;
        public float pointShrinkDuration = 0.3f;
        public float pointExpendDuration = 0.3f;
        public float pointSpawnDuration = 1f;
        public float pointDestroyDuration = 0.5f;
        
        [Title("Other")]
        public float pointsAndShapesSpawnDifference = 1.5f;
        public float cleanLevelDelay = 0.1f;
    }
}
