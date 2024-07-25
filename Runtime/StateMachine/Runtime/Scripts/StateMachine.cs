using System;
using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.StateMachines
{
    public abstract class StateMachine : MonoBehaviour
    {
        private State _currentState;
        private List<string> _stateHistory;
        protected virtual void Awake()
        {
            _stateHistory = new List<string>();

        }

        public void SwitchState(State newState)
        {
            //Debug.Log("SWITCHING STATE FROM " + _currentState?.ToString() + " TO : " + newState?.ToString());
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();

            _stateHistory.Add(_currentState?.GetName());
        }

        private void Update()
        {
            _currentState?.Tick();
        }
        public List<string> GetStateHistory()
        {
            return new List<string>(_stateHistory);
        }

    }
}