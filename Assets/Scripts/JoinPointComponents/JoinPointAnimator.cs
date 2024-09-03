using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;


namespace JoinPointComponents
{
    public class JoinPointAnimator : MonoBehaviour
    {
        [SerializeField] private float shapeStartAttractingDuration = 0.3f;
        [SerializeField] private float shapeStopAttractingDuration = 0.3f;



        public void AnimateSpawn()
        {
            transform.DOScale(Vector3.zero, 1).From().SetEase(Ease.OutBounce);
        }


        public async Task AnimateDestroy()
        {
            await transform.DOScale(Vector3.zero, 1).AsyncWaitForCompletion();
        }


        public void OnShapeAttracting()
        {
            transform.DOScale(0.5f, shapeStartAttractingDuration);
        }


        public void OnShapeNotAttracting()
        {
            transform.DOScale(1, shapeStopAttractingDuration);
        }
    }
}
