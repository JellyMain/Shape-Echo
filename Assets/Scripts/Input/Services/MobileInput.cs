using System;
using Input.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Input.Services
{
    public class MobileInput : IInput
    {
        private readonly InputActions inputActions;
        public event Action DragStarted;
        public event Action DragEnded;
        public event Action DashPressed;
        public event Action ShotPressed;
        public event Action ShotReleased;
        public event Action ReloadPressed;
        
        
        
        public MobileInput()
        {
            inputActions = new InputActions();

            inputActions.MobileInput.Enable();
            inputActions.MobileInput.DragStarted.performed += OnDragStarted;
            inputActions.MobileInput.DragStarted.canceled += OnDragEnded;
        }


        public Vector2 GetNormalizedMoveInput()
        {
            Vector2 touchPosition = inputActions.MobileInput.TouchPosition.ReadValue<Vector2>();
            return Camera.main.ScreenToWorldPoint(touchPosition);
        }


        public Vector2 GetWorldMousePosition()
        {
            throw new NotImplementedException();
        }


        


        private void OnDragStarted(InputAction.CallbackContext callbackContext)
        {
            DragStarted?.Invoke();
        }


        private void OnDragEnded(InputAction.CallbackContext callbackContext)
        {
            DragEnded?.Invoke();
        }
    }
}