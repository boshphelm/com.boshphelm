using UnityEngine;

namespace Boshphelm.HealthBars
{
    public class HealthBarProjector
    {
        private readonly GameObject _healthBar;
        private readonly HealthBarCameraTracker _cameraTracker;
        private readonly bool _tracking;
        private readonly bool _dontHide;

        private float _showTimer;

        public bool Showing { get; private set; }

        public HealthBarProjector(GameObject healthBar, bool dontHide, CameraTrackProps cameraTrackProps, bool tracking = true)
        {
            _dontHide = dontHide;
            _tracking = tracking;
            _healthBar = healthBar;
            _cameraTracker = new HealthBarCameraTracker(cameraTrackProps);
        }

        public void Show(float totalTimeToShow)
        {
            _showTimer = totalTimeToShow;
            Showing = true;
            _healthBar.SetActive(true);
        }

        public void Tick()
        {
            if (!Showing) return;

            if (_dontHide)
            {
                if (_tracking) _cameraTracker.Track();
            }
            else
            {
                _showTimer -= Time.deltaTime;

                if (_showTimer <= 0)
                {
                    Hide();
                }
                else
                {
                    if (_tracking) _cameraTracker.Track();
                }
            }
        }

        public void Hide()
        {
            _showTimer = 0f;
            Showing = false;

            _healthBar.SetActive(false);
        }
    }
}
