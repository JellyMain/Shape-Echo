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
        
        
        public PcInput()
        {
            inputActions = new InputActions();
            inputActions.PcInput.Enable();

            inputActions.PcInput.Dash.performed += Dash;
        }


        private void Dash(InputAction.CallbackContext callbackContext)
        {
            Dashed?.Invoke();
        }
        

        public Vector2 GetNormalizedMoveInput()
        {
            return inputActions.PcInput.Move.ReadValue<Vector2>();
        }


       
    }
}
