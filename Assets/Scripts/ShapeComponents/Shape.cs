using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using StaticData.Data;
using UnityEngine;


namespace ShapeComponents
{
    public class Shape : MonoBehaviour
    {
        [SerializeField] private ShapeAnimator shapeAnimator;
        [SerializeField] private ShapeID shapeID;
        public ShapeID ShapeID => shapeID;

        public Action ShapeSelected;
        public Action ShapeDeselected;


        private void Start()
        {
            shapeAnimator.AnimateSpawn();
        }


        private void OnEnable()
        {
            ShapeSelected += shapeAnimator.OnShapeSelected;
            ShapeDeselected += shapeAnimator.OnShapeDeselected;
        }


        private void OnDisable()
        {
            ShapeSelected -= shapeAnimator.OnShapeSelected;
            ShapeDeselected -= shapeAnimator.OnShapeDeselected;
        }


        public async UniTask Kill()
        {
            await shapeAnimator.AnimateDestroy();
            Destroy(gameObject);
        }
    }
}
