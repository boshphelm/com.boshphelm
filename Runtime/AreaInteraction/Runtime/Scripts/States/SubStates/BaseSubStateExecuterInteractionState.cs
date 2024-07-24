using Boshphelm.StateMachines;

namespace Boshphelm.AreaInteractionSystem
{
    public abstract class BaseSubStateExecuterInteractionState : BaseInteractionState
    {
        protected BaseSubStateExecuterInteractionState(InteractionAreaManager manager) : base(manager)
        {
        }
        protected BaseInteractionSubState currentSubState;

        public void ChangeSubState(BaseInteractionSubState subState)
        {
            currentSubState?.Exit();
            currentSubState = subState;
            currentSubState?.Enter();
        }

        public override void Tick()
        {
            currentSubState?.Tick();
        }
    }
}