using System;
using Infrastructure;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class ValidateButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        private LevelValidator levelValidator;


        private void Awake()
        {
            levelValidator = ServiceLocator.Container.Single<LevelValidator>();
        }


        private void OnEnable()
        {
            button.onClick.AddListener(levelValidator.ValidateLevel);
        }


        private void OnDisable()
        {
            button.onClick.RemoveListener(levelValidator.ValidateLevel);
        }
    }
}
