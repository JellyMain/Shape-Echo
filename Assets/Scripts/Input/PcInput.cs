using System;
using Input.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Input
{
    public class PcInput : IInput
    {
        private readonly InputActions inputActions;
        public event Action DragStarted;
        public event Action DragEnded;


        public PcInput()
        {
            inputActions = new InputActions();

            inputActions.PcInput.Enable();
            inputActions.PcInput.DragStarted.performed += OnDragStarted;
            inputActions.PcInput.DragStarted.canceled += OnDragEnded;
        }


        public Vector2 GetTouchWorldPosition()
        {
            Vector2 mousePosition = inputActions.PcInput.TouchPosition.ReadValue<Vector2>();
            return Camera.main.ScreenToWorldPoint(mousePosition);
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
