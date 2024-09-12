using System;
using UnityEngine;


namespace Input.Interfaces
{
    public interface IInput
    {
        public Vector2 GetNormalizedMoveInput();
        public Vector2 GetWorldMousePosition();
        public event Action Shot;
        public event Action Dashed;
    }
}