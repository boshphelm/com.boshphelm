using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.GFASystem
{
    public class MultipleActionExecuterAction : GameFlowAction, IGameFlowActionListener
    {
        [SerializeField] private List<GameFlowAction> _actions;

        private int _currentGameFlowActionIndex = 0;

        public override void StartAction()
        {
            base.StartAction();

            for (int i = 0; i < _actions.Count; i++) _actions[i].RegisterListener(this);

            if (_actions.Count > 0) StartCurrentGameActionFlow();
            else CompleteAction();
        }

        private void StartCurrentGameActionFlow()
        {
            GameFlowAction currentAction = _actions[_currentGameFlowActionIndex];
            currentAction.StartAction();
        }

        public void AddGameFlowAction(GameFlowAction gameFlowAction) { }

        public void OnGameFlowActionComplete(GameFlowAction gameFlowAction)
        {
            if (!_actions.Contains(gameFlowAction)) return;

            _currentGameFlowActionIndex = _currentGameFlowActionIndex + 1;
            gameFlowAction.UnregisterListener(this);

            if (_currentGameFlowActionIndex >= _actions.Count) CompleteAction();
            else StartCurrentGameActionFlow();
        }
    }
}