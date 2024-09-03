using System;
using UnityEngine;


namespace JoinPointComponents
{
    public class ShapeAttractor : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D attractionZone;
        [SerializeField] private float minMoveSpeed = 3;
        [SerializeField] private float maxMoveSpeed = 10;
        private Transform attractedShape;
        public event Action ShapeAttracting;
        public event Action ShapeNotAttracting;

        
        
        private void Update()
        {
            if (attractedShape != null)
            {
                AttractShape();
            }
        }

        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Shape") && attractedShape == null)
            {
                attractedShape = other.transform;
                ShapeAttracting?.Invoke();
            }
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Shape") && other.transform == attractedShape)
            {
                ShapeNotAttracting?.Invoke();
                attractedShape = null;
            }
        }
        
        
        
        private void AttractShape()
        {
            Vector2 targetPosition = transform.position;

            float distanceToObject = (attractedShape.position - transform.position).magnitude;

            float normalizeDistance = Mathf.Clamp01(distanceToObject / attractionZone.radius);

            float t = 1 - normalizeDistance;

            float moveSpeed = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, t);

            attractedShape.position = Vector2.Lerp(attractedShape.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
