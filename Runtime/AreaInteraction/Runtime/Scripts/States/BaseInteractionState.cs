using Boshphelm.StateMachines;

namespace Boshphelm.AreaInteractionSystem
{
    public abstract class BaseInteractionState : State
    {
        protected InteractionAreaManager manager;
        public float InteractionTimer { get; protected set; }

        protected BaseInteractionState(InteractionAreaManager manager)
        {
            this.manager = manager;
        }

        public abstract void Reset();
    }
}
