using System;
using UnityEngine;


namespace Input.Interfaces
{
    public interface IInput
    {
        public Vector2 GetNormalizedMoveInput();
        public event Action Dashed;
    }
}