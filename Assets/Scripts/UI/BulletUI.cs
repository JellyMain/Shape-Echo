using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Random = UnityEngine.Random;


namespace UI
{
    public class BulletUI : MonoBehaviour
    {
        [SerializeField] private Image activeBulletImage;
        [SerializeField] private float maxYOffset = 30f;
        [SerializeField] private float minYOffset = 20f;
        [SerializeField] private float maxXOffset = 10;
        [SerializeField] private float jumpDuration = 0.05f;
        [SerializeField] private float rotationDuration = 1f;
        [SerializeField] private float moveDuration = 1f;
        [SerializeField] private float fadeDuration = 1f;
        public bool IsActive { get; private set; } = true;
        private RectTransform rectTransform;
        private RectTransform activeBulletRectTransform;
        private Vector2 activeBulletStartPos;

        
        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            activeBulletRectTransform = activeBulletImage.GetComponent<RectTransform>();
            activeBulletStartPos = activeBulletRectTransform.anchoredPosition;
        }


        public async UniTask ActivateBullet()
        {
            IsActive = true;
            Vector2 startPos = rectTransform.anchoredPosition;

            await rectTransform.DOJumpAnchorPos(startPos, 10, 1, jumpDuration).AsyncWaitForCompletion();
            activeBulletRectTransform.anchoredPosition = activeBulletStartPos;
            activeBulletRectTransform.rotation = Quaternion.identity;
            activeBulletImage.DOFade(1, 0.5f);
        }


        public void DeactivateBullet()
        {
            IsActive = false;

            float randomXOffset = Random.Range(-maxXOffset, maxXOffset);
            float randomYOffset = Random.Range(minYOffset, maxYOffset);
            Vector2 startPos = activeBulletRectTransform.anchoredPosition;

            Vector2 targetPos = new Vector2(startPos.x + randomXOffset, startPos.y + randomYOffset);
            float targetAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            
            Vector3 targetRotation = new Vector3(0, 0, targetAngle);

            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(activeBulletRectTransform.DORotate(targetRotation, rotationDuration))
                .Insert(0, activeBulletRectTransform.DOAnchorPos(targetPos, moveDuration))
                .Insert(0, activeBulletImage.DOFade(0, fadeDuration));

            sequence.Play();

        }
    }
}
