using UnityEngine;

namespace boshphelm.Conditions
{
    public abstract class Condition : MonoBehaviour
    {
        public bool Active { get; protected set; }

        public bool Completed => _conditionSaveData.IsCompleted;

        private ConditionSaveData _conditionSaveData;

        public System.Action<Condition> OnConditionCompleted = _ => { };

        public virtual void Activate()
        {
            if (Completed) return;

            Active = true;
        }

        public virtual void Deactivate()
        {
            Active = false;
        }

        public virtual void Complete()
        {
            _conditionSaveData.IsCompleted = true;
            Deactivate();

            OnConditionCompleted.Invoke(this);
        }

        public virtual void Bind(ConditionSaveData conditionSaveData)
        {
            _conditionSaveData = conditionSaveData;
        }
    }

    [System.Serializable]
    public class ConditionSaveData
    {
        public bool IsCompleted;
    }
}