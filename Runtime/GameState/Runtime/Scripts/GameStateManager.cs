using System;
using System.Collections.Generic; 
using Boshphelm.StateMachines; 

namespace Boshphelm.GameStateSystem
{ 
    public class GameStateManager : StateMachine
    { 
        private Dictionary<Type, GameState> _states;  

        private void Awake() => InitializeStates(); 

        private void InitializeStates()
        {
            _states = new Dictionary<Type, GameState>
            {
                { typeof(MenuState), new MenuState(this) },
                { typeof(PlayState), new PlayState(this) },
                { typeof(FailState), new FailState(this) },
                { typeof(CompleteState), new CompleteState(this) }
            };
 
            SwitchState(_states[typeof(MenuState)]);
        } 

        public void GoToMenu() => SwitchState(_states[typeof(MenuState)]);
        public void StartGame() => SwitchState(_states[typeof(PlayState)]);
        public void GameOver() => SwitchState(_states[typeof(FailState)]);
        public void Complete() => SwitchState(_states[typeof(CompleteState)]);
 
    }
}
