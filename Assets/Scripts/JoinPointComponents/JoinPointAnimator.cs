using System;
using System.Threading.Tasks;
using DG.Tweening;
using Infrastructure;
using StaticData.Data;
using StaticData.Services;
using UnityEngine;


namespace JoinPointComponents
{
    public class JoinPointAnimator : MonoBehaviour
    {
        private StaticDataService staticDataService;
        
        
        private void Awake()
        {
            staticDataService = ServiceLocator.Container.Single<StaticDataService>();
        }


        public void AnimateSpawn()
        {
            float duration = staticDataService.AnimationsStaticData.pointSpawnDuration;
            transform.DOScale(Vector3.zero, duration).From().SetEase(Ease.OutBounce);
        }


        public async Task AnimateDestroy()
        {
            float duration = staticDataService.AnimationsStaticData.pointDestroyDuration;
            await transform.DOScale(Vector3.zero, duration).AsyncWaitForCompletion();
        }


        public void OnShapeAttracting(ShapeID _, int __)
        {
            float duration = staticDataService.AnimationsStaticData.pointShrinkDuration;
            transform.DOScale(0.5f, duration);
        }


        public void OnShapeAttracting()
        {
            float duration = staticDataService.AnimationsStaticData.pointShrinkDuration;
            transform.DOScale(0.5f, duration);
        }


        public void OnShapeNotAttracting(ShapeID _, int __)
        {
            float duration = staticDataService.AnimationsStaticData.pointExpendDuration;
            transform.DOScale(1, duration);
        }
        
        
        public void OnShapeNotAttracting()
        {
            float duration = staticDataService.AnimationsStaticData.pointExpendDuration;
            transform.DOScale(1, duration);
        }
    }
}
