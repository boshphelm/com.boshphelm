using UnityEngine;

namespace OffScreenIndicator
{
    public class ScreenPositionCalculator
    {
        public Vector3 WorldToScreenPoint(Camera mainCamera, Vector3 worldPosition) => mainCamera.WorldToScreenPoint(worldPosition);

        public bool IsPositionWithinScreenBounds(Vector3 screenPosition) =>
            screenPosition.z > 0 &&
            screenPosition.x > 0 &&
            screenPosition.x < Screen.width &&
            screenPosition.y > 0 &&
            screenPosition.y < Screen.height;

        public void CalculateIndicatorPositionAndAngle(ref Vector3 screenPosition, ref float angle, Vector3 screenCenter, Vector3 screenBounds)
        {
            screenPosition -= screenCenter;

            InvertPositionIfBehindCamera(ref screenPosition);

            angle = CalculateAngle(screenPosition);
            float slope = CalculateSlope(angle);

            ClampPositionToScreenBounds(ref screenPosition, slope, screenBounds);

            screenPosition += screenCenter;
        }

        private void InvertPositionIfBehindCamera(ref Vector3 screenPosition)
        {
            if (screenPosition.z < 0)
            {
                screenPosition *= -1;
            }
        }

        private float CalculateAngle(Vector3 position) => Mathf.Atan2(position.y, position.x);

        private float CalculateSlope(float angle) => Mathf.Tan(angle);

        private void ClampPositionToScreenBounds(ref Vector3 position, float slope, Vector3 bounds)
        {
            if (position.x > 0)
            {
                position = new Vector3(bounds.x, bounds.x * slope, 0);
            }
            else
            {
                position = new Vector3(-bounds.x, -bounds.x * slope, 0);
            }

            if (position.y > bounds.y)
            {
                position = new Vector3(bounds.y / slope, bounds.y, 0);
            }
            else if (position.y < -bounds.y)
            {
                position = new Vector3(-bounds.y / slope, -bounds.y, 0);
            }
        }
    }
}
