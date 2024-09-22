using System.Collections.Generic;
using System.Linq;
using Constants;
using EnemyComponents;
using StaticData.Data;
using UnityEngine;


namespace StaticData.Services
{
    public class StaticDataService
    {
        public PlayerStaticData PlayerStaticData { get; private set; }

        public Dictionary<EnemyType, EnemyStaticData> enemiesStaticData =
            new Dictionary<EnemyType, EnemyStaticData>();


        public void Init()
        {
            LoadStaticData();
        }


        private void LoadStaticData()
        {
            LoadPlayerStaticData();
            LoadEnemiesStaticData();
        }


        public EnemyStaticData EnemyStaticDataForEnemyType(EnemyType enemyType)
        {
            return enemiesStaticData.GetValueOrDefault(enemyType);
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
