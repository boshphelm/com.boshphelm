using UnityEngine;
using Cinemachine;
using Boshphelm.StateMachines;

namespace Boshphelm.CameraState
{
    public abstract class BaseCameraState : State 
    {
        protected readonly CameraStateManager Manager;
        protected readonly CinemachineVirtualCamera VirtualCamera; 
        protected Transform Target;

        protected BaseCameraState(
            CameraStateManager manager, 
            CinemachineVirtualCamera virtualCamera,  
            Transform target)
        {
            Manager = manager;
            VirtualCamera = virtualCamera; 
            Target = target;
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
 
    }
}