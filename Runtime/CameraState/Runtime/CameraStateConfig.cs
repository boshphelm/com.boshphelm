using UnityEngine;

namespace Boshphelm.CameraState
{ 
    [CreateAssetMenu(fileName = "CameraStateConfig", menuName = "Boshphelm/Camera/CameraStateConfig")]
    public class CameraStateConfig : ScriptableObject 
    {
        [Header("Camera Settings")]
        public float fieldOfView = 60f;
        public Vector3 positionOffset = Vector3.zero;
        public Vector3 rotationOffset = Vector3.zero;
        
        [Header("Follow Settings")]
        public float followSpeed = 5f;
        public float rotationSpeed = 5f;
        
        [Header("Blend Settings")]
        public float blendDuration = 1f;
        public AnimationCurve blendCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    }
}