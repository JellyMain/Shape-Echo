using System;
using System.Collections.Generic;
using System.IO;
using Dungeon;
using Sirenix.OdinInspector;
using UnityEngine;


namespace StaticData.Data
{
    [CreateAssetMenu(menuName = "StaticData/DungeonRoomsConfig", fileName = "DungeonRoomsConfig")]
    public class DungeonRoomsConfig : SerializedScriptableObject
    {
        public Dictionary<RoomType, Dictionary<RoomName, RoomBase>> dungeonRoomsConfig =
            new Dictionary<RoomType, Dictionary<RoomName, RoomBase>>();


        [Button]
        private void AddRoomPrefabs()
        {
            RoomBase[] allRooms = Resources.LoadAll<RoomBase>("Prefabs/Rooms");

            for (int i = 0; i < Enum.GetValues(typeof(RoomName)).Length; i++)
            {
                RoomBase currentRoomBase = allRooms[i];
                RoomName currentRoomName = currentRoomBase.RoomName;
                RoomType currentRoomType = currentRoomBase.RoomType;

                if (!dungeonRoomsConfig.ContainsKey(currentRoomType))
                {
                    dungeonRoomsConfig[currentRoomType] = new Dictionary<RoomName, RoomBase>();
                }

                if (!dungeonRoomsConfig[currentRoomType].ContainsKey(currentRoomName))
                {
                    dungeonRoomsConfig[currentRoomType][currentRoomName] = currentRoomBase;
                }
            }
        }
    }
}
