using System;
using System.Collections.Generic; 
using Boshphelm.StateMachines; 

namespace Boshphelm.GameStateSystem
{ 
    public class GameStateManager : StateMachine
    { 
        private List<GameState> _states = new List<GameState>();  

        private void Awake() => InitializeStates(); 

        private void InitializeStates()
        { 
            _states.AddRange(GetComponentsInChildren<GameState>());  
            if (_states.Count == 0) return;
            SwitchState(_states[0]);
        } 

        public void SwitchState(GameState state) => SwitchState(state);  
 
    }
}
