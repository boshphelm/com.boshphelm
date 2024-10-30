using System.Collections.Generic;
using Boshphelm.Conditions;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Tutorial
{
    public abstract class Tutorial : MonoBehaviour
    {
        [SerializeField] private List<TutorialCondition> _tutorialConditions;
        [SerializeField] private List<TutorialCondition> _conditionsByOrder;
        [SerializeField] protected TutorialFadeManager _tutorialFadeManager;

        private TutorialSaveData _tutorialSaveData = new TutorialSaveData();
        public bool Completed => _tutorialSaveData.IsCompleted;

        public System.Action<Tutorial> OnTutorialCompleted = _ => { };
        [Header("Broadcasting")] [SerializeField] private VoidEventChannel _onSave;

        public virtual void Bind(TutorialSaveData tutorialSaveData)
        {
            if (tutorialSaveData.ConditionSaveDatas == null)
            {
                var conditionSaveDatas = new List<ConditionSaveData>();

                foreach (TutorialCondition condition in _tutorialConditions)
                {
                    ConditionSaveData conditionSaveData = new ConditionSaveData
                    {
                        IsCompleted = false
                    };

                    conditionSaveDatas.Add(conditionSaveData);
                }

                var conditionsByOrderSaveDatas = new List<ConditionSaveData>();
                foreach (TutorialCondition conditionByOrder in _conditionsByOrder)
                {
                    ConditionSaveData conditionSaveData = new ConditionSaveData
                    {
                        IsCompleted = false
                    };

                    conditionsByOrderSaveDatas.Add(conditionSaveData);
                }

                tutorialSaveData.ConditionSaveDatas = conditionSaveDatas;
                tutorialSaveData.ConditionsByOrderSaveDatas = conditionsByOrderSaveDatas;
            }


            _tutorialSaveData = tutorialSaveData;

            for (int i = 0; i < _tutorialConditions.Count; i++)
            {
                _tutorialConditions[i].Bind(_tutorialSaveData.ConditionSaveDatas[i]);
            }

            for (int i = 0; i < _conditionsByOrder.Count; i++)
            {
                _conditionsByOrder[i].Bind(_tutorialSaveData.ConditionsByOrderSaveDatas[i]);
            }
        }

        public virtual void ListenAllConditionsAndStartTutorial()
        {
            if (IsAllConditionsCompleted())
            {
                StartTutorial();
            }
            else
            {
                ListenCurrentConditionByOrder();

                foreach (TutorialCondition tutorialCondition in _tutorialConditions)
                {
                    tutorialCondition.OnConditionCompleted += OnTutorialConditionCompleted;
                    tutorialCondition.Activate();
                }
            }
        }

        private void ListenCurrentConditionByOrder()
        {
            if (_tutorialSaveData.ConditionsByOrderIndex >= _conditionsByOrder.Count) return;

            //Debug.Log("LISTENING CONDITION");
            TutorialCondition conditionByOrder = _conditionsByOrder[_tutorialSaveData.ConditionsByOrderIndex];
            conditionByOrder.OnConditionCompleted += OnConditionByOrderCompleted;
            conditionByOrder.Activate();
        }

        private void OnConditionByOrderCompleted(Condition conditionByOrder)
        {
//            Debug.Log("CONDITIONBY ORDER COMPLETED : " + conditionByOrder, conditionByOrder);
            conditionByOrder.OnConditionCompleted -= OnConditionByOrderCompleted;
            _tutorialSaveData.ConditionsByOrderIndex++;

            if (IsAllConditionsCompleted())
            {
                StartTutorial();
            }
            else
            {
                ListenCurrentConditionByOrder();
            }
        }

        private void OnTutorialConditionCompleted(Condition completedCondition)
        {
            if (IsAllConditionsCompleted())
            {
                StartTutorial();
            }
            else
            {
                completedCondition.OnConditionCompleted -= OnTutorialConditionCompleted;
            }
        }

        public bool IsAllConditionsCompleted()
        {
            foreach (TutorialCondition condition in _tutorialConditions)
            {
                if (condition.Completed) continue;

                return false;
            } 
            
            foreach (TutorialCondition conditionByOrder in _conditionsByOrder)
            {
                if (conditionByOrder.Completed) continue;

                return false;
            }

            return true;
        }

        public abstract void StartTutorial();

        public virtual void CompleteTutorial()
        {
            _tutorialSaveData.IsCompleted = true;
            OnTutorialCompleted.Invoke(this);
            _onSave.RaiseEvent();
        }
    }
}