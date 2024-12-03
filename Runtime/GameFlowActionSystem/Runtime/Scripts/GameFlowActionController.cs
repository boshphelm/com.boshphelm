using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.GFASystem
{
    public class GameFlowActionController : MonoBehaviour, IGameFlowActionListener
    {
        [SerializeField] private List<GameFlowAction> _actions;

        public List<GameFlowAction> Actions => _actions;

        public System.Action onAllGameFlowActionsComplete;

        private List<GameFlowAction> _completedActions = new List<GameFlowAction>();

        int _currentGameFlowActionIndex = 0;

        private void Start()
        {
            for (int i = 0; i < _actions.Count; i++) _actions[i].RegisterListener(this);
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _actions.Count; i++)
            {
                _actions[i].UnregisterListener(this);
                _currentGameFlowActionIndex = 0;
            }
        }

        public void AddGameFlowAction(GameFlowAction gameFlowAction)
        {
            if (_actions.Contains(gameFlowAction)) return;

            _actions.Add(gameFlowAction);
            gameFlowAction.RegisterListener(this);
        }

        public void StartGameFlowAction()
        {
            _currentGameFlowActionIndex = 0;
            StartCurrentGameActionFlow();
        }

        private void StartNextGameFlowAction()
        {
            _currentGameFlowActionIndex = (_currentGameFlowActionIndex + 1) % _actions.Count;
            StartCurrentGameActionFlow();
        }

        private void StartCurrentGameActionFlow()
        {
            GameFlowAction currentAction = _actions[_currentGameFlowActionIndex];
            currentAction.StartAction();
        }

        public void OnGameFlowActionComplete(GameFlowAction gameFlowAction)
        {
            if (!_actions.Contains(gameFlowAction)) return;

            gameFlowAction.UnregisterListener(this);
            if (!_completedActions.Contains(gameFlowAction)) _completedActions.Add(gameFlowAction);

            bool isAllGameFlowActionsCompleted = IsAllActionsCompleted();
            if (isAllGameFlowActionsCompleted) OnAllGameFlowComplete();
            else StartNextGameFlowAction();
        }

        private bool IsAllActionsCompleted()
        {
            for (int i = 0; i < _actions.Count; i++)
            {
                if (!_completedActions.Contains(_actions[i])) return false;
            }

            return true;
        }

        private void OnAllGameFlowComplete()
        {
            onAllGameFlowActionsComplete?.Invoke();
        }

#if UNITY_EDITOR
        public void ClearList()
        {
            _actions.Clear();
        }
#endif
    }
}