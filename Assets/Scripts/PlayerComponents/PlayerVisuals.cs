using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


namespace PlayerComponents
{
    public class PlayerVisuals : MonoBehaviour
    {
        [SerializeField, Required] private TrailRenderer trailRenderer;
        [SerializeField, Required] private PlayerBase playerBase;
        [SerializeField, Required] private ParticleSystem particles;
        [SerializeField, Required] private Image reloadProgressBar;
        [SerializeField] private float squeezeAmount = 0.6f;
        [SerializeField] private float squeezeDuration = 0.1f;
        [SerializeField] private float unsqueezeDuration = 0.3f;
        private float reloadTime;


        private void OnEnable()
        {
            playerBase.playerMovement.DashStarted += EnableTrail;
            playerBase.playerMovement.DashEnded += DisableTrail;
            playerBase.playerShooting.ReloadStarted += OnReloadStarted;
            playerBase.playerShooting.Reloaded += StopReloading;
        }



        private void OnDisable()
        {
            playerBase.playerMovement.DashStarted -= EnableTrail;
            playerBase.playerMovement.DashEnded -= DisableTrail;
        }


        private void Update()
        {
            if (playerBase.playerMovement.isMoving)
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


        private void OnReloadStarted(float reloadDuration)
        {
            StartReloading(reloadDuration).Forget();
        }



        private async UniTaskVoid StartReloading(float reloadDuration)
        {
            reloadTime = 0;

            while (reloadTime < reloadDuration)
            {
                reloadTime += Time.deltaTime;
                float reloadProgress = reloadTime / reloadDuration;

                reloadProgressBar.fillAmount = reloadProgress;

                await UniTask.Yield();
            }

            reloadProgressBar.fillAmount = 0;
        }



        private void StopReloading(int _)
        {
            reloadTime = 0;
        }
    }
}
