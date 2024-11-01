using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Boshphelm.Utility
{ 
    public class InstantAnimator : IProgressAnimator
    {
        private readonly Slider slider;
        private readonly Image fillImage;

        public InstantAnimator(Slider slider)
        {
            this.slider = slider;
            this.fillImage = slider.fillRect?.GetComponent<Image>();
        }

        public void Animate(float fromValue, float toValue)
        {
            slider.value = toValue;
        }

        public void AnimateColor(Color fromColor, Color toColor)
        {
            if (fillImage != null)
                fillImage.color = toColor;
        }
    }

    public class SmoothAnimator : IProgressAnimator
    {
        private readonly Slider slider;
        private readonly Image fillImage;
        private readonly float duration;
        private readonly Ease easeType;
        private Tweener progressTween;
        private Tweener colorTween;

        public SmoothAnimator(Slider slider, float duration = 0.5f, Ease easeType = Ease.OutCubic)
        {
            this.slider = slider;
            this.fillImage = slider.fillRect?.GetComponent<Image>();
            this.duration = duration;
            this.easeType = easeType;
        }

        public void Animate(float fromValue, float toValue)
        {
            progressTween?.Kill();
            progressTween = DOTween.To(() => slider.value, x => slider.value = x, toValue, duration)
                .SetEase(easeType);
        }

        public void AnimateColor(Color fromColor, Color toColor)
        {
            if (fillImage != null)
            {
                colorTween?.Kill();
                colorTween = fillImage.DOColor(toColor, duration)
                    .SetEase(easeType);
            }
        }
    }
}
