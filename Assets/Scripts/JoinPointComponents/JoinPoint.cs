using System;
using Infrastructure;
using StaticData.Data;
using UnityEngine;


namespace JoinPointComponents
{
    [RequireComponent(typeof(ShapeAttractor), typeof(JoinPointAnimator))]
    public class JoinPoint : MonoBehaviour
    {
        [SerializeField] private ShapeAttractor shapeAttractor;
        [SerializeField] private JoinPointAnimator pointAnimator;
        private LevelValidator levelValidator;
        public JoinPointAnimator PointAnimator => pointAnimator;
        public int SpawnIndex { get; set; }


        private void Awake()
        {
            levelValidator = ServiceLocator.Container.Single<LevelValidator>();
        }


        private void Start()
        {
            pointAnimator.AnimateSpawn();
        }


        private void OnEnable()
        {
            shapeAttractor.ShapeAttracting += levelValidator.AddShape;
            shapeAttractor.ShapeNotAttracting += levelValidator.RemoveShape;
            shapeAttractor.ShapeAttracting += pointAnimator.OnShapeAttracting;
            shapeAttractor.ShapeNotAttracting += pointAnimator.OnShapeNotAttracting;
        }


        private void OnDisable()
        {
            shapeAttractor.ShapeAttracting -= levelValidator.AddShape;
            shapeAttractor.ShapeNotAttracting -= levelValidator.RemoveShape;
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
