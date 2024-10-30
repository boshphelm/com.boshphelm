using DG.Tweening;
using UnityEngine;

namespace Boshphelm.ProgressBar
{
    public class SmoothProgressBar : BaseProgressBar
    {
        [Header("Animation Settings")]
        [SerializeField] protected float animationDuration = 0.5f;
        [SerializeField] protected Ease animationEase = Ease.OutCubic;
        [SerializeField] private float _delay;
        private Tweener progressTween;
        private Tweener progressColor;

        public override void SetColor(Color targetColor)
        {
            progressColor?.Kill(); 
            progressColor = fillImage.DOColor(targetColor, animationDuration).SetDelay(_delay)
                .SetEase(animationEase);
        }

        protected override void UpdateVisuals()
        {
            progressTween?.Kill(); 
            progressTween = fillImage.DOFillAmount(currentFillAmount, animationDuration).SetDelay(_delay)
                .SetEase(animationEase);
        }
    }
}
 
