using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;


namespace ShapeComponents
{
    public class ShapeAnimator : MonoBehaviour
    {
        [SerializeField] private Ease animationEase;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float enterExitAnimationSpeed;

        private const string PULSE_FACTOR = "_PulseFactor";
        private const string INSIDE_WAVE_STRENGTH = "_InsideWaveStrength";

        private Material material;
        private Sequence animationSequence;
        private Sequence shapeEnteredSequence;
        private Sequence shapeExitedSequence;
        private bool canAnimate = true;
        

        private void Awake()
        {
            material = spriteRenderer.material;
        }
        

        public void AnimateSpawn()
        {
            transform.DOScale(Vector3.zero, 1).From().SetEase(animationEase);
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

            shapeEnteredSequence
                .Append(material.DOFloat(1, PULSE_FACTOR, enterExitAnimationSpeed));
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

            shapeExitedSequence
                .Append(material.DOFloat(0, PULSE_FACTOR, enterExitAnimationSpeed));
        }

        
        public async UniTask AnimateDestroy()
        {
            await transform.DOScale(0, 0.5f).AsyncWaitForCompletion().AsUniTask();
        }
    
        
        private void OnShapeClickedAnimation()
        {
            if (canAnimate)
            {
                transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.5f);
            }
        }


    
    }
}