using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Dungeon
{
    public abstract class RoomBase : SerializedMonoBehaviour
    {
        [SerializeField] private GameObject wallPrefab;
        [SerializeField] protected int widthRight;
        [SerializeField] protected int widthLeft;
        [SerializeField] protected int heightTop;
        [SerializeField] protected int heightBottom;
        [SerializeField] protected RoomName roomName;
        [SerializeField] protected RoomType roomType;
        [SerializeField] protected Dictionary<DoorDirection, Door> doorsDirections;

        public int WidthRight => widthRight;
        public int WidthLeft => widthLeft;
        public int HeightTop => heightTop;
        public int HeightBottom => heightBottom;
        public RoomType RoomType => roomType;
        public RoomName RoomName => roomName;
        public Dictionary<DoorDirection, Door> DoorsDirections => doorsDirections;



        public void CloseDoors(List<DoorDirection> openDoors)
        {
            var wallOffsets = new Dictionary<DoorDirection, Vector2[]>
            {
                { DoorDirection.Right, new[] { Vector2.down, Vector2.zero, Vector2.up } },
                { DoorDirection.Left, new[] { Vector2.down, Vector2.zero, Vector2.up } },
                { DoorDirection.Top, new[] { Vector2.right, Vector2.zero, Vector2.left } },
                { DoorDirection.Bottom, new[] { Vector2.right, Vector2.zero, Vector2.left } }
            };

            foreach (var direction in doorsDirections.Keys)
            {
                if (!openDoors.Contains(direction))
                {
                    Vector2 doorPosition = doorsDirections[direction].transform.position;

                    foreach (var offset in wallOffsets[direction])
                    {
                        Instantiate(wallPrefab, doorPosition + offset, Quaternion.identity);
                    }
                }
            }
        }



        [Button]
        private void SetDoors()
        {
            List<Door> doors = GetComponentsInChildren<Door>().ToList();
            doorsDirections = doors.ToDictionary(d => d.DoorDirection, d => d);
        }
    }
}
