using UnityEngine;

namespace boshphelm.Commands
{
    public abstract class Command : MonoBehaviour
    {
        [SerializeField] private bool _completeDirectly;

        public System.Action<Command> onCommandComplete = command => { };

        protected float percentage;
        public float Percentage => percentage;

        public abstract void StartCommand();

        public virtual void CompleteCommand()
        {
            percentage = 1f;

            onCommandComplete.Invoke(this);
            onCommandComplete = command => { };
        }
        public abstract void ResetCommand();
    }
}