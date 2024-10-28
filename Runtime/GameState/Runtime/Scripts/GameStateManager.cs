using System;
using System.Collections.Generic;
using Boshphelm.Panel;
using Boshphelm.StateMachines;
using UnityEngine;

namespace Boshphelm.GameStateSystem
{ 
    public class GameStateManager : StateMachine
    {
        [SerializeField] private GamePanelService _panelService;  
        private Dictionary<Type, GameState> _states; 
        public event Action<GameState> OnStateChanged;

        private void Awake() => InitializeStates(); 

        private void InitializeStates()
        {
            _states = new Dictionary<Type, GameState>
            {
                { typeof(MenuState), new MenuState(this, _panelService) },
                { typeof(PlayState), new PlayState(this, _panelService) },
                { typeof(FailState), new FailState(this, _panelService) },
                { typeof(CompleteState), new CompleteState(this, _panelService) }
            };
 
            ChangeState(_states[typeof(MenuState)]);
        }

        private void ChangeState(GameState gameState)
        {
            SwitchState(gameState);
            OnStateChanged?.Invoke(gameState);
        }

        public void GoToMenu() => ChangeState(_states[typeof(MenuState)]);
        public void StartGame() => ChangeState(_states[typeof(PlayState)]);
        public void GameOver() => ChangeState(_states[typeof(FailState)]);
        public void Complete() => ChangeState(_states[typeof(CompleteState)]);
 
    }
}
