using System;
using Input.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Input
{
    public class PcInput : IInput
    {
        private readonly InputActions inputActions;
        public event Action Dashed;
        public event Action Shot;
        
        
        public PcInput()
        {
            inputActions = new InputActions();
            inputActions.PcInput.Enable();

            inputActions.PcInput.Dash.performed += Dash;
            inputActions.PcInput.Shoot.performed += Shoot;
        }


        private void Dash(InputAction.CallbackContext callbackContext)
        {
            Dashed?.Invoke();
        }


        private void Shoot(InputAction.CallbackContext callbackContext)
        {
            Shot?.Invoke();
        }

        
        
        public Vector2 GetNormalizedMoveInput()
        {
            return inputActions.PcInput.Move.ReadValue<Vector2>();
        }


        public Vector2 GetWorldMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(inputActions.PcInput.MousePos.ReadValue<Vector2>());
        }
       
    }
}
