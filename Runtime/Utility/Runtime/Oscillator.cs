using UnityEngine;

namespace Boshphelm.Utility
{
    public class Oscillator : MonoBehaviour
    {
        [Tooltip("Oyun başladığı anda çalışsın mı.")][SerializeField] private bool _playOnAwake = true;

        [Header("Movement Variables..")]
        [Tooltip("Objenin belirlenen periyod içerisinde hareketini yapacağı yön.")]
        [SerializeField]
        private Vector3 _movementVector;

        [Header("Rotation Variables..")][SerializeField] private bool _willRotate;
        [Tooltip("Objenin belirlenen periyod içerisinde hareketini yapacağı rotasyon.")][SerializeField] private Vector3 _rotationVector;
        [Tooltip("Objenin pozitif yönde bir tur hareketini tamamlayacağı süre.")][SerializeField] private float _positivePeriod = 1f;
        [Tooltip("Objenin negatif yönde bir tur hareketini tamamlayacağı süre.")][SerializeField] private float _negativePeriod = 1f;

        private Vector3 _startPos;
        private Quaternion _startRot;
        private float _movementFactor, _timer, _prevRawsinWave;
        private const float _tau = Mathf.PI * 2;
        private bool _isActive, _isPositive = true;

        private void Start()
        {
            _startPos = transform.localPosition;
            _startRot = transform.localRotation;

            if (_playOnAwake) SetStatus(true);
        }

        private void Update()
        {
            if (!_isActive) return;

            float period = _isPositive ? _positivePeriod : _negativePeriod;
            _timer += Time.deltaTime / period;

            Oscillate();
        }

        private void Oscillate()
        {
            float rawSinWave = Mathf.Sin(_timer * _tau);
            _movementFactor = (rawSinWave + 1) / 2f;

            Vector3 offset = _movementVector * _movementFactor;
            transform.localPosition = _startPos + offset;

            if (_willRotate)
            {
                Quaternion rotationOffSet = Quaternion.Euler(_rotationVector * _movementFactor);
                transform.localRotation = _startRot * rotationOffSet;
            }

            _isPositive = _prevRawsinWave < rawSinWave;
            _prevRawsinWave = rawSinWave;
        }

        public void SetStatus(bool status)
        {
            _isActive = status;
        }
    }
}