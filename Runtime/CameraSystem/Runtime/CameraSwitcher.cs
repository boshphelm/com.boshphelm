using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

namespace boshphelm.CameraSystem
{
    public class CameraSwitcher : MonoBehaviour
    {
        [SerializeField] private CinemachineBlendDefinition _defaultBlendDefinition;
        [SerializeField] private CinemachineVirtualCameraBase _defaultCamera;
        [SerializeField] private float _defaultSpeed = 2;
        public static CameraSwitcher Instance;

        private static List<CinemachineVirtualCameraBase> cameras = new List<CinemachineVirtualCameraBase>();
        private static CinemachineVirtualCameraBase activeCamera;
        public CinemachineVirtualCameraBase ActiveCamera => activeCamera;

        private Camera _mainCameraCompenent;
        public Camera MainCameraCompenent => _mainCameraCompenent;
        private CinemachineBrain _cinemachineBrain;
        public CinemachineBrain CinemachineBrain => _cinemachineBrain;

        private void Awake()
        {
            if (_cinemachineBrain != null) return;
            Instance = this;
            _mainCameraCompenent = FindAnyObjectByType<Camera>();
            _cinemachineBrain = FindObjectOfType<CinemachineBrain>();

            Register(_defaultCamera);
        }

        public void SetDefaultCameraWithDefaultBlend()
        {
            if (!cameras.Contains(_defaultCamera)) Register(_defaultCamera);
            SetDefaultCamera(_defaultBlendDefinition);
        }

        public void SetDefaultCamera(CinemachineBlendDefinition blendDefinition)
        {
            SwitchCamera(_defaultCamera, blendDefinition, blendDefinition.m_Time);
        }

        public bool IsActiveCamera(CinemachineVirtualCameraBase camera) => camera == activeCamera;

        public void SwitchCamera(CinemachineVirtualCameraBase camera, float switchSpeed = .5f)
        {
            SwitchCamera(camera, _defaultBlendDefinition, switchSpeed);
        }

        public void SwitchCamera(CinemachineVirtualCameraBase camera, CinemachineBlendDefinition blendDefinition, float switchSpeed = .5f)
        {
            if (camera == null || IsActiveCamera(camera)) return;

            Register(camera);
            camera.Priority = 10;
            _cinemachineBrain.m_DefaultBlend = blendDefinition;
            _cinemachineBrain.m_DefaultBlend.m_Time = switchSpeed;
            activeCamera = camera;

            foreach (CinemachineVirtualCameraBase cam in cameras)
            {
                if (cam != camera) cam.Priority = 0;
            }
        }

        public void SwitchDirectly(CinemachineVirtualCameraBase camera)
        {
            if (camera == null || IsActiveCamera(camera)) return;

            Register(camera);
            camera.Priority = 10;

            _cinemachineBrain.m_DefaultBlend.m_Time = 0f;
            activeCamera = camera;

            foreach (CinemachineVirtualCameraBase cam in cameras)
            {
                if (cam != camera) cam.Priority = 0;
            }
        }


        public void Register(CinemachineVirtualCameraBase camera)
        {
            if (cameras.Contains(camera)) return;

            cameras.Add(camera);
        }

        public void Unregister(CinemachineVirtualCameraBase camera)
        {
            if (!cameras.Contains(camera)) return;

            cameras.Remove(camera);
        }
    }
}