namespace Boshphelm.AreaInteractionSystem
{
    public class CompletedState : BaseInteractionState
    {
        public CompletedState(InteractionAreaManager manager) : base(manager) { }

        public override void Enter()
        {
            InteractionTimer = manager.InteractionCompleteDuration;

            manager.onInteractionCompleted?.Invoke();

            manager.SwitchState(new InactiveState(manager));
        }

        public override void Exit()
        {
        }

        public override void Reset()
        {
        }

        public override void Tick()
        {
        }
        //public override string GetName() => "Completed";
    }
}
