using UnityEngine;
using UnityEngine.UI;

namespace boshphelm.Utility
{
    public class HealthBarFillUI : MonoBehaviour
    {
        [SerializeField] private Image _directlyFillImage;
        [SerializeField] private Image _fillImage;
        [SerializeField] private float _fillSpeed = 2f;

        [SerializeField] private Color _minHpColor;
        [SerializeField] private Color _middleHpColor;
        [SerializeField] private Color _maxHpColor;

        private bool _filling;
        private float _targetFillAmount;
        private float _startFillAmount;
        private float _timer;

        public void UpdateFillAmount(float fillAmount)
        {
            _filling = true;
            _startFillAmount = _fillImage.fillAmount;
            _targetFillAmount = fillAmount;

            _directlyFillImage.fillAmount = fillAmount;
            _directlyFillImage.color = HpColor(fillAmount);
        }

        private void Update()
        {
            if (!_filling) return;

            _timer += Time.deltaTime * _fillSpeed;
            float progress = Mathf.Lerp(_startFillAmount, _targetFillAmount, _timer);

            _fillImage.fillAmount = progress;


            if (_timer >= 1f) CompleteFilling();
        }

        private Color HpColor(float progress)
        {
            if (progress > .5f) return Color.Lerp(_middleHpColor, _maxHpColor, (progress - .5f) * 2f);
            return Color.Lerp(_minHpColor, _middleHpColor, progress * 2f);
        }

        private void CompleteFilling()
        {
            _filling = false;
            _timer = 0f;
        }
    }
}