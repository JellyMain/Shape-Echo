using System;
using Infrastructure;
using Input.Interfaces;
using UnityEngine;
using Zenject;


namespace PlayerComponents
{
    public class EyesMover : MonoBehaviour
    {
        [SerializeField] private float boundsRadius = 0.1f;
        [SerializeField] private Transform firstEye;
        [SerializeField] private Transform secondEye;

        private Vector2 firstEyeStartPos;
        private Vector2 secondEyeStartPos;
        private IInput inputService;


        [Inject]
        private void Construct(IInput inputService)
        {
            this.inputService = inputService;
        }


        private void Start()
        {
            firstEyeStartPos = firstEye.localPosition;
            secondEyeStartPos = secondEye.localPosition;
        }


        private void Update()
        {
            MoveEyes(firstEye, firstEyeStartPos);
            MoveEyes(secondEye, secondEyeStartPos);
        }


        private void MoveEyes(Transform eye, Vector2 eyeStartPos)
        {
            Vector2 mousePosition = inputService.GetWorldMousePosition();

            Vector2 eyeWorldPos = (Vector2)transform.position + eyeStartPos;
            Vector2 direction = mousePosition - eyeWorldPos;

            Vector2 clampedDirection = Vector2.ClampMagnitude(direction, boundsRadius);

            eye.localPosition = eyeStartPos + clampedDirection;
        }
    }
}
