using UnityEngine;

namespace OffScreenIndicator
{
    public class OffScreenIndicatorCore
    {
        public OffScreenIndicatorCore()
        {
        }

        public Vector3 GetScreenPosition(Camera mainCamera, Vector3 targetPosition)
        {
            return mainCamera.WorldToScreenPoint(targetPosition);
        }
        public bool IsTargetVisible(Vector3 screenPosition)
        {
            return screenPosition.z > 0 && screenPosition.x > 0 && screenPosition.x < Screen.width && screenPosition.y > 0 && screenPosition.y < Screen.height;
        }
        public void GetPointerIndicatorPositionAndAngle(ref Vector3 screenPosition, ref float angle, Vector3 screenCentre, Vector3 screenBounds)
        {
            AdjustScreenPositionForCentre(ref screenPosition, screenCentre);
            InvertScreenPositionIfBehindCamera(ref screenPosition);
            CalculateAngleAndSlope(screenPosition, out angle, out float slope);
            AdjustScreenPositionForBounds(ref screenPosition, slope, screenBounds);
            screenPosition += screenCentre; // Bring the position back to its original reference
        }
        private void AdjustScreenPositionForCentre(ref Vector3 screenPosition, Vector3 screenCentre)
        {
            screenPosition -= screenCentre;
        }
        private void InvertScreenPositionIfBehindCamera(ref Vector3 screenPosition)
        {
            if (screenPosition.z < 0)
            {
                screenPosition *= -1;
            }
        }
        private void CalculateAngleAndSlope(Vector3 screenPosition, out float angle, out float slope)
        {
            angle = Mathf.Atan2(screenPosition.y, screenPosition.x);
            slope = Mathf.Tan(angle);
        }
        private void AdjustScreenPositionForBounds(ref Vector3 screenPosition, float slope, Vector3 screenBounds)
        {
            if (screenPosition.x > 0)
            {
                screenPosition = new Vector3(screenBounds.x, screenBounds.x * slope, 0);
            }
            else
            {
                screenPosition = new Vector3(-screenBounds.x, -screenBounds.x * slope, 0);
            }

            if (screenPosition.y > screenBounds.y)
            {
                screenPosition = new Vector3(screenBounds.y / slope, screenBounds.y, 0);
            }
            else if (screenPosition.y < -screenBounds.y)
            {
                screenPosition = new Vector3(-screenBounds.y / slope, -screenBounds.y, 0);
            }
        }
    }
}
