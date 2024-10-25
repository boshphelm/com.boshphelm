using UnityEngine;
using Cinemachine;
using Boshphelm.StateMachines;

namespace Boshphelm.CameraState
{    
    public class CameraStateManager : StateMachine 
    {
        [Header("Cameras")]
        [SerializeField] private CinemachineVirtualCamera _idleCamera; 
          
        private IdleCameraState _idleState; 

        private void Start() => Init();

        private void Init()
        {
            _idleState = new IdleCameraState(this, _idleCamera, null);  
            SwitchState(_idleState);
        } 
    }  
}
 