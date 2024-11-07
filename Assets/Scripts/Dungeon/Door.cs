using System;
using Constants;
using UnityEngine;


namespace Dungeon
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private DoorDirection doorDirection;
        public DoorDirection DoorDirection => doorDirection;

        public event Action DoorEntered;


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(RuntimeConstants.Tags.PLAYER))
            {
                DoorEntered?.Invoke();
            }
        }
    }
}
