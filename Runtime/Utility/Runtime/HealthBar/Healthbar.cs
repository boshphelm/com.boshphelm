using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Boshphelm.Utility
{
    public class Healthbar : MonoBehaviour
    {
        [SerializeField] private GameObject _healthBar;
        [SerializeField] private Image _healthBarImage;
        [SerializeField] private Image _animatedHealthBarImage;

        [SerializeField] private HealthBarFillUI _healthBarFillUI;
        //        [SerializeField] private Color _minHealthColor = Color.red;
        //    [SerializeField] private Color _maxHealthColor = Color.green;

        private float _maxHealth;
        private float _healthBarShowTimer;
        private Camera _camera;
        public bool _active;

        public void Setup(float maxHealth)
        {
            _maxHealth = maxHealth;

            _healthBarImage.fillAmount = 1;
            _animatedHealthBarImage.fillAmount = 1;

            _healthBar.SetActive(false);

            _camera = Camera.main;
        }

        public void OnHealthChange(float currentHealth)
        {
            _active = true;
            _healthBar.SetActive(true);
            _healthBarShowTimer = 1;

            UpdateHealthBar(currentHealth);
            if (currentHealth <= 0) _healthBar.SetActive(false);
        }

        private void UpdateHealthBar(float currentHealth)
        {
            float healthPercentage = currentHealth / _maxHealth;
            _healthBarFillUI.UpdateFillAmount(healthPercentage);
            //_healthBarImage.fillAmount = _healthPercentage;
            //_animatedHealthBarImage.FillAmountAnimation(_healthPercentage, .5f, DG.Tweening.Ease.OutSine);

            //   Color targetColor = Color.Lerp(_minHealthColor, _maxHealthColor, _healthPercentage);
        }

        private void Update()
        {
            if (!_active) return;

            _healthBar.transform.LookAt(_camera.transform);

            _healthBarShowTimer -= Time.deltaTime;

            if (_healthBarShowTimer <= 0)
            {
                _healthBar.SetActive(false);
                _active = false;
                _healthBarShowTimer = 0;
            }
        }
    }
}