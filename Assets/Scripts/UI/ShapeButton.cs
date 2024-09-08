using System;
using Infrastructure;
using ShapeComponents;
using StaticData.Data;
using StaticData.Services;
using UnityEngine;
using UnityEngine.EventSystems;


namespace UI
{
    public class ShapeButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ShapeID shapeID;
        private StaticDataService staticDataService;
        private ShapeSpawner shapeSpawner;


        private void Awake()
        {
            staticDataService = ServiceLocator.Container.Single<StaticDataService>();
            shapeSpawner = ServiceLocator.Container.Single<ShapeSpawner>();
        }

        

        private void OnButtonClick()
        {
            shapeSpawner.SpawnShape(shapeID);
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            OnButtonClick();
        }
    }
}
