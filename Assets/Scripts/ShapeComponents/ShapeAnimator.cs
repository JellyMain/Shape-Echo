using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Infrastructure;
using StaticData.Services;
using UnityEngine;


namespace ShapeComponents
{
    public class ShapeAnimator : MonoBehaviour
    {
        [SerializeField] private Ease animationEase;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private const string PULSE_FACTOR = "_PulseFactor";
        private const string INSIDE_WAVE_STRENGTH = "_InsideWaveStrength";

        private Material material;
        private Sequence animationSequence;
        private Sequence shapeEnteredSequence;
        private Sequence shapeExitedSequence;
        private bool canAnimate = true;
        private StaticDataService staticDataService;

        
        private void Awake()
        {
            staticDataService = ServiceLocator.Container.Single<StaticDataService>();
            material = spriteRenderer.material;
        }
        

        public void AnimateSpawn()
        {
            float duration = staticDataService.AnimationsStaticData.shapeSpawnDuration;
            transform.DOScale(Vector3.zero, duration).From().SetEase(animationEase);
        }

        
        public void OnShapeSelected()
        {
            if (!canAnimate) return;

            canAnimate = false;

            if (shapeEnteredSequence != null)
            {
                shapeEnteredSequence.Restart();
                return;
            }

            shapeEnteredSequence =
                DOTween.Sequence().SetSpeedBased().SetAutoKill(false).OnComplete(() => canAnimate = true);

            float duration = staticDataService.AnimationsStaticData.shapePulsingDuration;
            
            shapeEnteredSequence
                .Append(material.DOFloat(1, PULSE_FACTOR, duration));
        }


        public void OnShapeDeselected()
        {
            if (!canAnimate) return;

            canAnimate = false;

            if (shapeExitedSequence != null)
            {
                shapeExitedSequence.Restart();
                return;
            }

            shapeExitedSequence = DOTween.Sequence().SetSpeedBased().SetAutoKill(false).OnComplete(() => canAnimate = true);

            float duration = staticDataService.AnimationsStaticData.shapeStopPulsingDuration;
            
            shapeExitedSequence
                .Append(material.DOFloat(0, PULSE_FACTOR, duration));
        }

        
        public async UniTask AnimateDestroy()
        {
            float duration = staticDataService.AnimationsStaticData.shapeDestroyDuration;
            await transform.DOScale(0, duration).AsyncWaitForCompletion().AsUniTask();
        }
    }
}