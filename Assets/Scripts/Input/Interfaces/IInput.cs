using System;
using UnityEngine;


namespace Input.Interfaces
{
    public interface IInput
    {
        public Vector2 GetTouchWorldPosition();
        public event Action DragStarted;
        public event Action DragEnded;
    }
}