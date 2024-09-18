using System;
using UnityEngine;


namespace EnemyComponents
{
    public class EnemyHealth : MonoBehaviour
    {
        public float Max { get; set; }
        public float Current { get; set; }

        public event Action Damaged; 

        
        public void TakeDamage(float damage)
        {
            if (Current > 0)
            {
                Current -= damage;
                Damaged?.Invoke();
            }
        }
    }
}