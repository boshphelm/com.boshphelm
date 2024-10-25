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
            CameraStateConfig config,
            Transform target) 
            : base(manager, virtualCamera, config, target)
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
            ApplyConfig();
        }

        public override void Exit()
        {
            VirtualCamera.Priority = 0;
        }

        public override void Tick()
        {
            UpdateCameraPosition();
        }

        protected override void ApplyConfig()
        {
            base.ApplyConfig();

            if (_transposer != null)
            {
                _transposer.m_FollowOffset = Config.positionOffset;
                _transposer.m_XDamping = Config.followSpeed;
                _transposer.m_YDamping = Config.followSpeed;
                _transposer.m_ZDamping = Config.followSpeed;
            }

            if (_composer != null)
            {
                _composer.m_TrackedObjectOffset = Config.rotationOffset;
                _composer.m_HorizontalDamping = Config.rotationSpeed;
                _composer.m_VerticalDamping = Config.rotationSpeed;
            }
        }

        private void UpdateCameraPosition()
        {
            if (Target == null) return;

            // Burada ihtiyaca göre ek kamera pozisyon/rotasyon güncellemeleri yapılabilir
            // Örneğin input'a göre offset'i değiştirmek gibi
        }
    }
}