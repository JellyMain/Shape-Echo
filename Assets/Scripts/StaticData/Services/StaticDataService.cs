using System.Collections.Generic;
using System.Linq;
using Constants;
using EnemyComponents;
using StaticData.Data;
using UI;
using UnityEngine;
using Weapons.Bullets;


namespace StaticData.Services
{
    public class StaticDataService
    {
        public PlayerStaticData PlayerStaticData { get; private set; }

        private Dictionary<EnemyType, EnemyStaticData> enemiesStaticData =
            new Dictionary<EnemyType, EnemyStaticData>();

        private Dictionary<AmmoType, BulletUI> bulletsUIConfig;


        public void Init()
        {
            LoadStaticData();
        }


        private void LoadStaticData()
        {
            LoadBulletSlotsConfig();
            LoadPlayerStaticData();
            LoadEnemiesStaticData();
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
    }
}
