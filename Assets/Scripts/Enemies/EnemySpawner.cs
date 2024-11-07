using EnemyComponents;
using Factories;
using UnityEngine;
using Zenject;


namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyType enemyType;
        private EnemiesFactory enemiesFactory;


        [Inject]
        private void Construct(EnemiesFactory enemiesFactory)
        {
            this.enemiesFactory = enemiesFactory;
        }
        

        public void Spawn()
        {
            enemiesFactory.CreateEnemy(enemyType, transform.position);
        }
    }
}
