using UnityEngine;

namespace Boshphelm.StateMachines
{
    public abstract class StateMachine : MonoBehaviour
    {
        private State _currentState;

        public void SwitchState(State newState)
        {
            //Debug.Log("SWITCHING STATE FROM " + _currentState?.ToString() + " TO : " + newState?.ToString());
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        private void Update()
        {
            _currentState?.Tick();
        }
    }
}