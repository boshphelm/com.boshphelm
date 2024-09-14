using UnityEngine;

namespace Boshphelm.HealthBars
{
    public class HealthBarCameraTracker
    {
        private readonly Transform _healthBarTransform;
        private readonly Transform _cameraTransform;
        private readonly Vector3 _initialRotation;
        private readonly bool _rotateX, _rotateY, _rotateZ;

        public HealthBarCameraTracker(CameraTrackProps cameraTrackProps)
        {
            _healthBarTransform = cameraTrackProps.HealthBarTransform;
            _cameraTransform = cameraTrackProps.CameraTransform;
            _initialRotation = _healthBarTransform.rotation.eulerAngles;
            _rotateX = cameraTrackProps.RotateX;
            _rotateY = cameraTrackProps.RotateY;
            _rotateZ = cameraTrackProps.RotateZ;
        }

        public void Track()
        {
            /*var direction = (_cameraTransform.position - _healthBarTransform.position).normalized;
            float angle = Mathf.Atan2(direction.z, direction.x);

            var newAngle = _offset;
            newAngle.y += angle;

            _healthBarTransform.rotation = Quaternion.Euler(newAngle);*/

            var directionToCamera = _cameraTransform.position - _healthBarTransform.position;
            directionToCamera.Normalize();

            var lookRotation = Quaternion.LookRotation(directionToCamera);
            var targetRotation = lookRotation.eulerAngles;

            if (!_rotateX) targetRotation.x = _initialRotation.x;
            if (!_rotateY) targetRotation.y = _initialRotation.y;
            if (!_rotateZ) targetRotation.z = _initialRotation.z;

            _healthBarTransform.rotation = Quaternion.Euler(targetRotation);
        }
    }

    [System.Serializable]
    public class CameraTrackProps
    {
        public Transform HealthBarTransform;
        public Transform CameraTransform;
        public bool RotateX;
        public bool RotateY;
        public bool RotateZ;

    }
}
