namespace Boshphelm.AreaInteractionSystem
{
    public class InactiveState : BaseInteractionState
    {
        public InactiveState(InteractionAreaManager manager) : base(manager) { }

        public override void Enter()
        {
            InteractionTimer = 0;
            manager.SetFillAmount(0);
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
        public override string GetName() => "Inactive";
    }
}
