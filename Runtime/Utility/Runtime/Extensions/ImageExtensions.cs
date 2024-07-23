using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Boshphelm.Utility
{
    public static class ImageExtensions
    {
        public static void FillAmountAnimation(this Image image, float targetFillAmount, float duration, Ease ease, bool scaleAnimation = false, float scalePercentage = 0)
        {
            DOTween.To(() => image.fillAmount, x => image.fillAmount = x, targetFillAmount, duration).SetEase(ease);

            if (!scaleAnimation) return;
            float baseScale = image.transform.localScale.magnitude;
            float animatonScale = baseScale + (baseScale * scalePercentage) / 100;
            image.transform.DOScale(animatonScale, duration / 2).SetEase(ease).SetLoops(2, LoopType.Yoyo);
        }
        public static void ColorAnimation(this Image image, Color minColor, Color maxColor, float duration, Ease ease)
        {
            float fillAmount = image.fillAmount;
            Color targetColor = Color.Lerp(minColor, maxColor, fillAmount);

            DOTween.To(() => image.color, x => image.color = x, targetColor, duration).SetEase(ease);
        }
    }
}
