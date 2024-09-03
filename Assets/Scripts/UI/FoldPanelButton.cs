using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;


namespace UI
{
    public class FoldPanelButton : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform shapePanel;
        [SerializeField] private float animationDuration = 0.5f;
        private const float Y_FOLDED_POSITION = -59;
        private bool isFolded = true;
        private Vector2 dragStartWorldPosition;
        private Vector2 dragEndWorldPosition;


        public void OnPointerClick(PointerEventData eventData) { }


        private void Fold()
        {
            shapePanel.DOAnchorPos(new Vector2(0, Y_FOLDED_POSITION), animationDuration);
            isFolded = true;
        }


        private void Unfold()
        {
            shapePanel.DOAnchorPos(new Vector2(0, -Y_FOLDED_POSITION), animationDuration);
            isFolded = false;
        }


        public void OnDrag(PointerEventData eventData) { }


        public void OnBeginDrag(PointerEventData eventData)
        {
            GetStartPosition(eventData);
        }

        
        public void OnEndDrag(PointerEventData eventData)
        {
            GetEndPosition(eventData);

            Vector2 swipeDirection = SmoothSwipeDirection(dragStartWorldPosition, dragEndWorldPosition);

            ChangePanelState(swipeDirection);
        }


        private void GetStartPosition(PointerEventData eventData)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(eventData.position);
            dragStartWorldPosition = position;
        }


        private void GetEndPosition(PointerEventData eventData)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(eventData.position);
            dragEndWorldPosition = position;
        }


        private void ChangePanelState(Vector2 swipeDirection)
        {
            if (isFolded && swipeDirection == Vector2.up)
            {
                Unfold();
            }
            else if (!isFolded && swipeDirection == Vector2.down)
            {
                Fold();
            }
        }


        private Vector2 SmoothSwipeDirection(Vector2 startPosition, Vector2 endPosition)
        {
            Vector2 direction = (endPosition - startPosition).normalized;
            Vector2 smoothedSwipedDirection;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                smoothedSwipedDirection = new Vector2(Mathf.Sign(direction.x), 0);
            }
            else
            {
                smoothedSwipedDirection = new Vector2(0, Mathf.Sign(direction.y));
            }

            return smoothedSwipedDirection;
        }
    }
}
