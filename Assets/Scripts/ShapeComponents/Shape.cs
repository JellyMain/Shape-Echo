using System;
using UnityEngine;


namespace ShapeComponents
{
    public class Shape : MonoBehaviour
    {
        [SerializeField] private ShapeAnimator shapeAnimator;

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


        public void Kill()
        {
            
        }
    }
}
