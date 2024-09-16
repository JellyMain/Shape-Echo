using System;
using Input.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Input
{
    public class PcInput : IInput
    {
        private readonly InputActions inputActions;
        public event Action DashPressed;
        public event Action ShotPressed;
        public event Action ShotReleased;
        public event Action ReloadPressed;
        
        
        public PcInput()
        {
            inputActions = new InputActions();
            inputActions.PcInput.Enable();

            inputActions.PcInput.Dash.performed += Dash;
            inputActions.PcInput.Shoot.performed += Shoot;
            inputActions.PcInput.Shoot.canceled += StopShooting;
            inputActions.PcInput.Reload.performed += Reload;
        }


        private void Reload(InputAction.CallbackContext obj)
        {
            ReloadPressed?.Invoke();
        }


        private void Dash(InputAction.CallbackContext callbackContext)
        {
            DashPressed?.Invoke();
        }


        private void Shoot(InputAction.CallbackContext callbackContext)
        {
            ShotPressed?.Invoke();
        }

        
        private void StopShooting(InputAction.CallbackContext obj)
        {
            ShotReleased?.Invoke();
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
