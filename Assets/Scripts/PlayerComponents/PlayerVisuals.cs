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
        [SerializeField, Required] private ParticleSystem particles;
        [SerializeField] private float squeezeAmount = 0.6f;
        [SerializeField] private float squeezeDuration = 0.1f;
        [SerializeField] private float unsqueezeDuration = 0.3f;


        private void OnEnable()
        {
            playerMovement.DashStarted += EnableTrail;
            playerMovement.DashEnded += DisableTrail;
        }


        private void OnDisable()
        {
            playerMovement.DashStarted -= EnableTrail;
            playerMovement.DashEnded -= DisableTrail;
        }


        private void Update()
        {
            if (playerMovement.isMoving)
            {
                EnableMovingEffect();
            }
            else
            {
                DisableMovingEffect();
            }
        }

        private void EnableTrail()
        {
            trailRenderer.emitting = true;
        }

        private void DisableTrail()
        {
            trailRenderer.emitting = false;
        }

        
        private void EnableMovingEffect()
        {
            if (Mathf.Approximately(transform.localScale.y, squeezeAmount)) return;

            transform.DOScaleY(squeezeAmount, squeezeDuration).SetEase(Ease.OutQuad);
            transform.DOScaleX(1.2f, squeezeDuration).SetEase(Ease.OutQuad);
            particles.Play();

        }

        
        private void DisableMovingEffect()
        {
            if (Mathf.Approximately(transform.localScale.y, 1)) return;

            transform.DOScaleY(1, unsqueezeDuration).SetEase(Ease.OutQuad);
            transform.DOScaleX(1, unsqueezeDuration).SetEase(Ease.OutQuad);
            particles.Stop();
        }
    }
}
