using System.Collections.Generic;
using UnityEngine;


namespace StaticData.Data
{
    [CreateAssetMenu(fileName = "ShapesStaticData", menuName = "StaticData/ShapesStaticData")]
    public class ShapesStaticData: ScriptableObject
    {
        public List<ShapeConfig> shapeConfigs;
    }
}
