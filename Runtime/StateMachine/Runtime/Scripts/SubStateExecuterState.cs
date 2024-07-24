namespace Boshphelm.StateMachines
{
    public abstract class SubStateExecuterState : State
    {
        protected SubState currentSubState;

        public void ChangeSubState(SubState subState)
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
