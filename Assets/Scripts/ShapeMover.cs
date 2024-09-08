using System;
using Factories;
using Infrastructure;
using Infrastructure.Services;
using Input.Services;
using ShapeComponents;
using UnityEngine;
using UnityEngine.InputSystem;


public class ShapeMover : MonoBehaviour
{
    [SerializeField] private LayerMask shapesLayer;
    [SerializeField] private float moveSpeed = 0.5f;
    public Shape SelectedShape { get; set; }
    private InputService inputService;


    private void Awake()
    {
        inputService = ServiceLocator.Container.Single<InputService>();
    }


    private void OnEnable()
    {
        inputService.CurrentInput.DragStarted += SelectShape;
        inputService.CurrentInput.DragEnded += DeselectShape;
    }


    private void OnDisable()
    {
        inputService.CurrentInput.DragStarted -= SelectShape;
        inputService.CurrentInput.DragEnded -= DeselectShape;
    }


    private void Update()
    {
        if (SelectedShape != null)
        {
            Vector2 targetPosition = inputService.CurrentInput.GetTouchWorldPosition();
            float t = moveSpeed * Time.deltaTime;
            Vector2 newPosition = Vector2.Lerp(SelectedShape.transform.position, targetPosition, t);

            SelectedShape.transform.position = newPosition;
        }
        
    }


    private void SelectShape()
    {
        RaycastHit2D hit = Physics2D.Raycast(inputService.CurrentInput.GetTouchWorldPosition(), Vector2.zero,
            Mathf.Infinity, shapesLayer);

        if (hit)
        {
            SelectedShape = hit.collider.GetComponent<Shape>();
            SelectedShape.ShapeSelected?.Invoke();
        }
    }


    private void DeselectShape()
    {
        if (SelectedShape != null)
        {
            SelectedShape.ShapeDeselected?.Invoke();
            SelectedShape = null;
        }
    }
}