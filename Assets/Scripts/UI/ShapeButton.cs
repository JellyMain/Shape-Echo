using Infrastructure;
using ShapeComponents;
using StaticData.Data;
using StaticData.Services;
using UnityEngine;
using UnityEngine.EventSystems;


namespace UI
{
    public class ShapeButton : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        [SerializeField] private ShapeID shapeID;
        private ShapeMover shapeMover;
        private StaticDataService staticDataService;


        private void Awake()
        {
            staticDataService = ServiceLocator.Container.Single<StaticDataService>();
            shapeMover = ServiceLocator.Container.Single<ShapeMover>();
        }



        public void OnBeginDrag(PointerEventData eventData)
        {
            CreateAndSelectShape();
        }


        public void OnDrag(PointerEventData eventData) { }


        private void CreateAndSelectShape()
        {
            Shape prefab = staticDataService.ForShapeID(shapeID).shapePrefab;

            Vector2 buttonPosition = Camera.main.ScreenToWorldPoint(transform.position);
            Shape createdShape = Instantiate(prefab, buttonPosition, Quaternion.identity);
            shapeMover.SelectedShape = createdShape;
        }
    }
}
