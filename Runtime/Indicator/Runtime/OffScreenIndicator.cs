using System;
using System.Collections.Generic;
using UnityEngine;

namespace OffScreenIndicator
{
    [DefaultExecutionOrder(-1)]
    public class OffScreenIndicator : MonoBehaviour
    {
        [SerializeField] private float _screenBoundOffset = 0.9f;

        private Camera _mainCamera;
        private Vector3 _screenCenter;
        private Vector3 _screenBounds;
        private ScreenPositionCalculator _screenPositionCalculator;

        public static event Action<IndicatorTarget, bool> TargetStateChanged;
        private readonly HashSet<IndicatorTarget> _targets = new HashSet<IndicatorTarget>();

        private void Awake()
        {
            InitializeComponents();
        }

        private void OnEnable()
        {
            SubscribeToEvents();
            InitializeScreenParameters();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void LateUpdate()
        {
            UpdateIndicators();
        }

        private void InitializeComponents()
        {
            _screenPositionCalculator = new ScreenPositionCalculator();
        }

        private void SubscribeToEvents()
        {
            TargetStateChanged += HandleTargetStateChanged;
        }

        private void UnsubscribeFromEvents()
        {
            TargetStateChanged -= HandleTargetStateChanged;
        }

        private void InitializeScreenParameters()
        {
            _mainCamera = Camera.main;
            _screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;
            _screenBounds = _screenCenter * _screenBoundOffset;
        }

        private void UpdateIndicators()
        {
            foreach (var target in _targets)
            {
                UpdateSingleIndicator(target);
            }
        }

        private void UpdateSingleIndicator(IndicatorTarget target)
        {
            if (!target.NeedPointerIndicator) return;

            var screenPosition = GetScreenPosition(target);
            bool isTargetVisible = IsTargetVisible(screenPosition);
            float distanceFromCamera = GetDistanceFromCamera(target);

            if (!isTargetVisible)
            {
                UpdateOffScreenIndicator(target, ref screenPosition, distanceFromCamera);
            }
            else
            {
                DeactivateIndicator(target);
            }
        }

        private Vector3 GetScreenPosition(IndicatorTarget target) => _screenPositionCalculator.WorldToScreenPoint(_mainCamera, target.transform.position);

        private bool IsTargetVisible(Vector3 screenPosition) => _screenPositionCalculator.IsPositionWithinScreenBounds(screenPosition);

        private float GetDistanceFromCamera(IndicatorTarget target) => target.NeedDistanceText ? target.GetDistanceFromCamera(_mainCamera.transform.position) : float.MinValue;

        private void UpdateOffScreenIndicator(IndicatorTarget target, ref Vector3 screenPosition, float distanceFromCamera)
        {
            float angle = float.MinValue;
            _screenPositionCalculator.CalculateIndicatorPositionAndAngle(ref screenPosition, ref angle, _screenCenter, _screenBounds);

            var indicator = GetOrCreateIndicator(target);
            indicator.RefreshInnerObjectRotations();
            UpdateIndicatorPosition(indicator, screenPosition, angle, distanceFromCamera);
        }

        private PointerIndicator GetOrCreateIndicator(IndicatorTarget target)
        {
            if (target.indicator == null)
            {
                target.indicator = PointerObjectPool.Instance.GetPooledObject();
                InitializeIndicator(target.indicator, target);
            }
            return target.indicator;
        }

        private void InitializeIndicator(PointerIndicator indicator, IndicatorTarget target)
        {
            indicator.SetImageColor(target.TargetPointerColor);
            indicator.SetTargetImageSprite(target.TargetSprite);
            indicator.SetTargetImageSize(target.TargetSize);
        }

        private void UpdateIndicatorPosition(PointerIndicator indicator, Vector3 screenPosition, float angle, float distance)
        {
            indicator.SetDistanceText(distance);
            indicator.SetTextRotation(Quaternion.identity);
            indicator.transform.position = screenPosition;
            indicator.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        }

        private void DeactivateIndicator(IndicatorTarget target)
        {
            if (target.indicator != null)
            {
                target.indicator.Activate(false);
                target.indicator = null;
            }
        }

        private void HandleTargetStateChanged(IndicatorTarget target, bool active)
        {
            if (active)
            {
                _targets.Add(target);
            }
            else
            {
                DeactivateIndicator(target);
                _targets.Remove(target);
            }
        }

        public static void ChangeTargetState(IndicatorTarget target, bool active)
        {
            TargetStateChanged?.Invoke(target, active);
        }
    }
}
