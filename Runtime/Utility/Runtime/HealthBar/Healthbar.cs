using System;
using UnityEngine;

namespace Boshphelm.HealthBars
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private GameObject _healthBar;
        [SerializeField] private HealthBarFillUI _healthBarFillUI;
        [SerializeField] private CameraTrackProps _cameraTrackProps;
        [SerializeField] private bool _trackingTheCamera;
        [SerializeField] private bool _dontHide;

        private float _maxHealth;
        private HealthBarProjector _projector;

        private const float _healthBarProjectionTime = 1f;

        public void Setup(float maxHealth)
        {
            _projector = new HealthBarProjector(_healthBar, _dontHide, _cameraTrackProps, _trackingTheCamera);
            if (_dontHide)
            {
                _projector.Show(0f);
            }
            else
            {
                _projector.Hide();
            }

            _maxHealth = maxHealth;

            _healthBarFillUI.FillDirectly();
            _healthBarFillUI.UpdateHealthText(_maxHealth);
        }

        public void OnHealthChange(float currentHealth)
        {
            UpdateHealthBar(currentHealth);
            if (currentHealth <= 0)
            {
                _projector.Hide();
            }
            else
            {
                _projector.Show(_healthBarProjectionTime);
            }
        }

        private void UpdateHealthBar(float currentHealth)
        {
            float healthPercentage = currentHealth / _maxHealth;

            _healthBarFillUI.UpdateFillAmount(healthPercentage);
            _healthBarFillUI.UpdateHealthText(currentHealth);
        }

        private void Update()
        {
            if (_projector == null) return;

            if (_projector.Showing)
            {
                _projector.Tick();
            }
        }
    }
}
