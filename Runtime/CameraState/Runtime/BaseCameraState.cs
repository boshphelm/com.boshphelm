using UnityEngine;
using Cinemachine;
using Boshphelm.StateMachines;

namespace Boshphelm.CameraState
{
    public abstract class BaseCameraState : State 
    {
        protected readonly CameraStateManager Manager;
        protected readonly CinemachineVirtualCamera VirtualCamera;
        protected CameraStateConfig Config;
        protected Transform Target;

        protected BaseCameraState(
            CameraStateManager manager, 
            CinemachineVirtualCamera virtualCamera, 
            CameraStateConfig config,
            Transform target)
        {
            Manager = manager;
            VirtualCamera = virtualCamera;
            Config = config;
            Target = target;
        }

        public virtual void UpdateConfig(CameraStateConfig newConfig)
        {
            Config = newConfig;
            ApplyConfig();
        }

        public virtual void UpdateTarget(Transform newTarget)
        {
            Target = newTarget;
            if (VirtualCamera != null)
            {
                VirtualCamera.Follow = Target;
                VirtualCamera.LookAt = Target;
            }
        }

        protected virtual void ApplyConfig()
        {
            if (VirtualCamera == null) return;

            var composer = VirtualCamera.GetCinemachineComponent<CinemachineComposer>();
            if (composer != null)
            {
                composer.m_TrackedObjectOffset = Config.positionOffset;
            }

            VirtualCamera.m_Lens.FieldOfView = Config.fieldOfView;
        }
    }
}