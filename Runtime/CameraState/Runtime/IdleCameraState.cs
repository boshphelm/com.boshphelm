using UnityEngine;
using Cinemachine;

namespace Boshphelm.CameraState
{
    public class IdleCameraState : BaseCameraState 
    {
        private CinemachineTransposer _transposer;
        private CinemachineComposer _composer;

        public IdleCameraState(
            CameraStateManager manager, 
            CinemachineVirtualCamera virtualCamera,  
            Transform target) 
            : base(manager, virtualCamera, target)
        {
            _transposer = VirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            _composer = VirtualCamera.GetCinemachineComponent<CinemachineComposer>();
            
            if (_transposer == null)
                _transposer = VirtualCamera.AddCinemachineComponent<CinemachineTransposer>();
            
            if (_composer == null)
                _composer = VirtualCamera.AddCinemachineComponent<CinemachineComposer>();
        }

        public override void Enter()
        {
            VirtualCamera.Priority = 10;
            UpdateTarget(Target); 
        }

        public override void Exit()
        {
            VirtualCamera.Priority = 0;
        }

        public override void Tick()
        { 
        } 
 
    }
}