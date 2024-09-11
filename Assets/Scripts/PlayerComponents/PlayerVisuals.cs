using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;


namespace PlayerComponents
{
    public class PlayerVisuals : MonoBehaviour
    {
        [SerializeField, Required] private TrailRenderer trailRenderer;
        [SerializeField, Required] private PlayerMovement playerMovement;
        private bool canDeform;
        

        private void OnEnable()
        {
            playerMovement.DashStarted += EnableTrail;
            playerMovement.DashEnded += DisableTrial;
        }


        private void OnDisable()
        {
            playerMovement.DashStarted -= EnableTrail;
            playerMovement.DashEnded -= DisableTrial;
        }


        private void EnableTrail()
        {
            trailRenderer.emitting = true;
        }


        private void DisableTrial()
        {
            trailRenderer.emitting = false;
        }


        private void SqueezePlayer()
        {
            if (playerMovement.isMoving)
            {
                transform.DOScaleY(0.6f, 0.5f);
            }
        }
    }
}
