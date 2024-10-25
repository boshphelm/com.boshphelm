using UnityEngine;
using Cinemachine;
using Boshphelm.StateMachines;

namespace Boshphelm.CameraState
{ 
    public class CameraStateManager : StateMachine 
    {
        [Header("Required Components")]
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private CameraStateConfig _config;
        
        [Header("Target Settings")]
        [SerializeField] private Transform _target;

        private IdleCameraState _idleState;

        private void Start() => InitializeStates();

        private void InitializeStates()
        {
            IdleCameraState();
        }

        private void IdleCameraState()
        {
            _idleState = new IdleCameraState(this, _virtualCamera, _config, _target);
            SwitchState(_idleState);
        }
    }  
}