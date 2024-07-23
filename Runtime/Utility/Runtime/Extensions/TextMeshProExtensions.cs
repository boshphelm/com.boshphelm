using DG.Tweening;
using TMPro;

namespace Boshphelm.Utility
{
    public static class TextMeshProExtensions
    {
        public static void ValueIncreaser(this TextMeshProUGUI valueText, int valueTarget, int valueStart, float duration, Ease ease, bool scaleAnimation = false, string frontString = null, string backString = null, float delay = 0)
        {
            int value = valueStart;
            float _valueIncreaseDuration = duration;
            DOTween.To(() => value, x => value = x, valueTarget, _valueIncreaseDuration)
                .SetEase(ease)
                .SetDelay(delay)
                .OnUpdate(() =>
                {
                    valueText.text = frontString + value.ToString() + backString;
                });

            if (scaleAnimation)
                valueText.transform.DOScale(.25f, duration / 2).SetEase(ease).SetRelative(true).SetLoops(2, LoopType.Yoyo);
        }
    }
}
