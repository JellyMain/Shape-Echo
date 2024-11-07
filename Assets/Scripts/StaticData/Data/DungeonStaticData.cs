using System.Collections.Generic;
using Dungeon;
using Sirenix.OdinInspector;
using UnityEngine;


namespace StaticData.Data
{
    [CreateAssetMenu(menuName = "StaticData/DungeonStaticData", fileName = "DungeonStaticData")]
    public class DungeonStaticData : SerializedScriptableObject
    {
        public int gridSize = 100;
        public float edgeAddChance = 12f;
        public int roomsMargin = 8;

        public Dictionary<RoomType, int> roomTypesCount;
        

        public LayerMask obstacleLayer;
        public uint initialPenalty = 10;
        public float smoothingFactor = 10;
        public int smoothingSegmentLength = 40;
    }
}
