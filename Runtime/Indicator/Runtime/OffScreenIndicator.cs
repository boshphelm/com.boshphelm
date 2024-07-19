using System;
using System.Collections.Generic;
using UnityEngine;

namespace OffScreenIndicator
{
    [DefaultExecutionOrder(-1)]
    public class OffScreenIndicator : MonoBehaviour
    {
        [SerializeField] private float screenBoundOffset = 0.9f;

        private Camera _mainCamera;
        private Vector3 _screenCenter;
        private Vector3 _screenBounds;

        private HashSet<IndicatorTarget> _targets = new HashSet<IndicatorTarget>();

        public static event Action<IndicatorTarget, bool> TargetStateChanged;
        private OffScreenIndicatorCore _offScreenIndicatorCore;

        private void Awake()
        {
            _offScreenIndicatorCore = new OffScreenIndicatorCore();
        }

        private void OnEnable()
        {
            TargetStateChanged += HandleTargetStateChanged;

            _mainCamera = Camera.main;
            _screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;
            _screenBounds = _screenCenter * screenBoundOffset;
        }

        private void OnDisable()
        {
            TargetStateChanged -= HandleTargetStateChanged;
        }

        private void LateUpdate()
        {
            DrawIndicators();
        }

        private void DrawIndicators()
        {
            foreach (var target in _targets)
            {
                Vector3 screenPosition = _offScreenIndicatorCore.GetScreenPosition(_mainCamera, target.transform.position);
                bool isTargetVisible = _offScreenIndicatorCore.IsTargetVisible(screenPosition);
                float distanceFromCamera = target.NeedDistanceText ? target.GetDistanceFromCamera(_mainCamera.transform.position) : float.MinValue;

                var indicator = target.indicator;

                if (!target.NeedPointerIndicator) continue;

                if (!isTargetVisible)
                {
                    float angle = float.MinValue;
                    _offScreenIndicatorCore.GetPointerIndicatorPositionAndAngle(ref screenPosition, ref angle, _screenCenter, _screenBounds);
                    if (indicator == null)
                    {
                        indicator = GetIndicator(ref target.indicator);
                        InitializeIndicator(indicator, target);
                    }
                    indicator.RefreshInnerObjectRotations();
                    UpdateIndicator(indicator, screenPosition, angle, distanceFromCamera);
                }
                else if (indicator != null)
                {
                    indicator.Activate(false);
                    target.indicator = null;
                }
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
                DeactivateTarget(target);
                _targets.Remove(target);
            }
        }

        private PointerIndicator GetIndicator(ref PointerIndicator indicator)
        {
            if (indicator == null)
            {
                indicator = PointerObjectPool.Instance.GetPooledObject();
                indicator.Activate(true);
            }

            return indicator;
        }

        private void InitializeIndicator(PointerIndicator indicator, IndicatorTarget target)
        {
            indicator.SetImageColor(target.TargetPointerColor);
            indicator.SetTargetImageSprite(target.TargetSprite);
            indicator.SetTargetImageSize(target.TargetSize);
        }

        private void UpdateIndicator(PointerIndicator indicator, Vector3 position, float angle, float distance)
        {
            indicator.SetDistanceText(distance);
            indicator.SetTextRotation(Quaternion.identity);
            indicator.transform.position = position;
            indicator.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        }

        private void DeactivateTarget(IndicatorTarget target)
        {
            target.indicator?.Activate(false);
            target.indicator = null;
        }

        public static void ChangeTargetState(IndicatorTarget target, bool active)
        {
            TargetStateChanged?.Invoke(target, active);
        }
    }
}

