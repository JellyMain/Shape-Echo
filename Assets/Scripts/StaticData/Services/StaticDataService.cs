using System.Collections.Generic;
using System.Linq;
using Constants;
using Dungeon;
using EnemyComponents;
using StaticData.Data;
using UI;
using UnityEngine;
using Weapons.Bullets;
using Zenject;


namespace StaticData.Services
{
    public class StaticDataService: IInitializable
    {
        public PlayerStaticData PlayerStaticData { get; private set; }

        public DungeonStaticData DungeonStaticData { get; private set; }

        private Dictionary<EnemyType, EnemyStaticData> enemiesStaticData;

        private Dictionary<AmmoType, BulletUI> bulletsUIConfig;

        private Dictionary<RoomType, Dictionary<RoomName, RoomBase>> dungeonRoomsConfig;
        

        
        public void Initialize()
        {
            LoadStaticData();
        }
        

        private void LoadStaticData()
        {
            LoadDungeonStaticData();
            LoadBulletSlotsConfig();
            LoadPlayerStaticData();
            LoadEnemiesStaticData();
            LoadDungeonRoomsStaticData();
        }


        public Dictionary<RoomName, RoomBase> RoomsForRoomType(RoomType roomType)
        {
            if (dungeonRoomsConfig.TryGetValue(roomType, out Dictionary<RoomName, RoomBase> rooms))
            {
                return rooms;
            }

            Debug.LogError($"Couldn't find rooms dictionary with key: {roomType}");
            return null;
        }


        public EnemyStaticData EnemyStaticDataForEnemyType(EnemyType enemyType)
        {
            if (enemiesStaticData.TryGetValue(enemyType, out EnemyStaticData enemyStaticData))
            {
                return enemyStaticData;
            }

            Debug.LogError($"Couldn't found enemy static data with key: {enemyType}");
            return null;
        }


        public BulletUI BulletUIPrefabForAmmoType(AmmoType ammoType)
        {
            if (bulletsUIConfig.TryGetValue(ammoType, out BulletUI bulletUIPrefab))
            {
                return bulletUIPrefab;
            }

            Debug.LogError($"Couldn't found prefab with key: {ammoType}");
            return null;
        }


        private void LoadBulletSlotsConfig()
        {
            bulletsUIConfig = Resources.Load<BulletsUIConfig>(RuntimeConstants.StaticDataPaths.BULLET_SLOTS_CONFIG)
                .bulletsUIConfig;
        }


        private void LoadEnemiesStaticData()
        {
            enemiesStaticData =
                Resources.LoadAll<EnemyStaticData>(RuntimeConstants.StaticDataPaths.ENEMIES_STATIC_DATA)
                    .ToDictionary(x => x.enemyType, x => x);
        }


        private void LoadPlayerStaticData()
        {
            PlayerStaticData = Resources.Load<PlayerStaticData>(RuntimeConstants.StaticDataPaths.PLAYER_STATIC_DATA);
        }


        private void LoadDungeonRoomsStaticData()
        {
            dungeonRoomsConfig = Resources.Load<DungeonRoomsConfig>(RuntimeConstants.StaticDataPaths.DUNGEON_ROOMS_CONFIG)
                .dungeonRoomsConfig;
        }


        private void LoadDungeonStaticData()
        {
            DungeonStaticData = Resources.Load<DungeonStaticData>(RuntimeConstants.StaticDataPaths.DUNGEON_STATIC_DATA);
        }


       
    }
}
