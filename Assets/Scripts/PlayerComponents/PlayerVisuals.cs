using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Utils;


namespace PlayerComponents
{
    public class PlayerVisuals : MonoBehaviour
    {
        [SerializeField, Required] private PlayerBase playerBase;
        [SerializeField, Required] private TrailRenderer trailRenderer;
        [SerializeField, Required] private ParticleSystem particles;
        [SerializeField, Required] private Image reloadProgressBar;
        

        private void OnEnable()
        {
            playerBase.playerMovement.DashStarted += EnableTrail;
            playerBase.playerMovement.DashEnded += DisableTrail;
            playerBase.playerShooting.ReloadStarted += OnReloadStarted;
        }


        private void OnDisable()
        {
            playerBase.playerMovement.DashStarted -= EnableTrail;
            playerBase.playerMovement.DashEnded -= DisableTrail;
            playerBase.playerShooting.ReloadStarted -= OnReloadStarted;
        }


        private void Update()
        {
            if (playerBase.playerMovement.IsMoving)
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
            Vector2 moveDirection = playerBase.playerMovement.MoveDirection;

            Quaternion targetRotation = DataUtility.DirectionToQuaternion(moveDirection);
            particles.transform.rotation = targetRotation;

            if (!particles.isPlaying)
            {
                particles.Play();
            }
        }


        private void DisableMovingEffect()
        {
            if (particles.isPlaying)
            {
                particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }


        private void OnReloadStarted(float reloadDuration)
        {
            StartReloading(reloadDuration).Forget();
        }


        private async UniTaskVoid StartReloading(float reloadDuration)
        {
            while (playerBase.playerShooting.IsReloading)
            {
                float reloadProgress = playerBase.playerShooting.ReloadTimer / reloadDuration;

                reloadProgressBar.fillAmount = reloadProgress;

                await UniTask.Yield();
            }

            reloadProgressBar.fillAmount = 0;
        }
    }
}
