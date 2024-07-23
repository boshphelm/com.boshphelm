using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Boshphelm.Utility
{
    public class JoystickListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action OnJoystickPressed;
        public event Action OnJoystickHeldForDuration;

        private float _requiredHoldDuration;

        private bool _isPointerDown = false;
        private float _holdTimer = 0f;

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPointerDown = true;
            _holdTimer = 0f;
            OnJoystickPressed?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPointerDown = false;
            _holdTimer = 0f;
        }

        private void Update()
        {
            if (_isPointerDown)
            {
                _holdTimer += Time.deltaTime;

                if (_holdTimer >= _requiredHoldDuration)
                {
                    OnJoystickHeldForDuration?.Invoke();
                    _isPointerDown = false;
                }
            }
        }

        public void SetRequiredHoldDuration(float duration)
        {
            _requiredHoldDuration = duration;
        }
    }
}
