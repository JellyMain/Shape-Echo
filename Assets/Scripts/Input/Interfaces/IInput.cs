using System;
using UnityEngine;


namespace Input.Interfaces
{
    public interface IInput
    {
        public Vector2 GetNormalizedMoveInput();
        public Vector2 GetWorldMousePosition();
        public event Action ShotPressed;
        public event Action ShotReleased;
        public event Action DashPressed;
        public event Action ReloadPressed;
    }
}