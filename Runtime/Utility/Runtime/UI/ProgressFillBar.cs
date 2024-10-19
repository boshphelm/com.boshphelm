using UnityEngine;
using UnityEngine.UI;

namespace Boshphelm.Missions
{
    public class ProgressFillBar : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private float _fillSpeed = 2f;

        private bool _filling;
        private float _targetFillAmount;
        private float _startFillAmount;
        private float _timer;

        public void FillDirectly()
        {
            _filling = false;
            _fillImage.fillAmount = 1f;
        }

        public void EmptyDirectly()
        {
            _filling = false;
            _fillImage.fillAmount = 0f;
        }

        public void UpdateFillAmount(float fillAmount)
        {
            _filling = true;
            _startFillAmount = _fillImage.fillAmount;
            _targetFillAmount = fillAmount;
        }

        private void Update()
        {
            if (!_filling) return;

            _timer += Time.deltaTime * _fillSpeed;
            float progress = Mathf.Lerp(_startFillAmount, _targetFillAmount, _timer);

            _fillImage.fillAmount = progress;

            if (_timer >= 1f) CompleteFilling();
        }

        private void CompleteFilling()
        {
            _filling = false;
            _timer = 0f;
        }
    }
}
