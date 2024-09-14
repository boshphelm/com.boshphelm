using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Boshphelm.HealthBars
{
    public class HealthBarFillUI : MonoBehaviour
    {
        [SerializeField] private Image _directlyFillImage;
        [SerializeField] private Image _fillImage;
        [SerializeField] private float _fillSpeed = 2f;

        [SerializeField] private Color _minHpColor;
        [SerializeField] private Color _middleHpColor;
        [SerializeField] private Color _maxHpColor;

        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private bool _showText;

        private bool _filling;
        private float _targetFillAmount;
        private float _startFillAmount;
        private float _timer;

        public void FillDirectly()
        {
            _filling = false;

            _directlyFillImage.fillAmount = 1f;
            _fillImage.fillAmount = 1f;
            UpdateTextVisibility();
        }

        public void EmptyDirectly()
        {
            _filling = false;

            _directlyFillImage.fillAmount = 0f;
            _fillImage.fillAmount = 0f;
            UpdateHealthText(0f);
        }

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

        public void UpdateHealthText(float health)
        {
            if (_healthText == null) return;
            _healthText.text = health.ToString();
        }

        public void SetShowText(bool show)
        {
            _showText = show;
            UpdateTextVisibility();
        }

        private void UpdateTextVisibility()
        {
            if (_healthText == null) return;
            _healthText.gameObject.SetActive(_showText);
        }
    }
}