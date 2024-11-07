using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
using Enemies;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Dungeon
{
    public class EnemyRoom : RoomBase
    {
        [SerializeField] private List<EnemySpawner> enemySpawners;


        private void OnEnable()
        {
            foreach (Door door in doorsDirections.Values)
            {
                door.DoorEntered += SpawnEnemies;
            }
        }


        private void OnDisable()
        {
            foreach (Door door in DoorsDirections.Values)
            {
                door.DoorEntered -= SpawnEnemies;
            }
        }

        

        private void SpawnEnemies()
        {
            foreach (EnemySpawner spawner in enemySpawners)
            {
                spawner.Spawn();
            }
        }



        [Button]
        private void SetSpawners()
        {
            enemySpawners = GetComponentsInChildren<EnemySpawner>().ToList();
        }
    }
}
