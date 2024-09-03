using System;
using UnityEngine;


namespace JoinPointComponents
{
    [RequireComponent(typeof(ShapeAttractor), typeof(JoinPointAnimator))]
    public class JoinPoint : MonoBehaviour
    {
        [SerializeField] private ShapeAttractor shapeAttractor;
        [SerializeField] private JoinPointAnimator pointAnimator;


        private void Start()
        {
            pointAnimator.AnimateSpawn();
        }


        private void OnEnable()
        {
            shapeAttractor.ShapeAttracting += pointAnimator.OnShapeAttracting;
            shapeAttractor.ShapeNotAttracting += pointAnimator.OnShapeNotAttracting;
        }


        private void OnDisable()
        {
            shapeAttractor.ShapeAttracting -= pointAnimator.OnShapeAttracting;
            shapeAttractor.ShapeNotAttracting -= pointAnimator.OnShapeNotAttracting;
        }


        public async void Kill()
        {
            await pointAnimator.AnimateDestroy();
            Destroy(gameObject);
        }
    }
}
