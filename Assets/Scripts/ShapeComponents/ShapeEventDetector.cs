using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace ShapeComponents
{
    public class ShapeEventDetector : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler,
        IPointerEnterHandler, IPointerExitHandler
    {
        public event Action ShapeClicked;
        public event Action ShapeStartedDrag;
        public event Action ShapeEndedDrag;
        public event Action<PointerEventData> ShapeDragged;
        public event Action ShapeEntered;
        public event Action ShapeExited;


        public void OnPointerClick(PointerEventData eventData)
        {
            ShapeClicked?.Invoke();
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            ShapeStartedDrag?.Invoke();
        }


        public void OnEndDrag(PointerEventData eventData)
        {
            ShapeEndedDrag?.Invoke();
        }


        public void OnDrag(PointerEventData eventData)
        {
            ShapeDragged?.Invoke(eventData);
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            ShapeEntered?.Invoke();
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            ShapeExited?.Invoke();
        }
    }
}
