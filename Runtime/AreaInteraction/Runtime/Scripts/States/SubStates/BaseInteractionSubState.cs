using Boshphelm.StateMachines;

namespace Boshphelm.AreaInteractionSystem
{
    public class BaseInteractionSubState : SubState
    {
        protected InteractionAreaManager manager;
        public BaseInteractionSubState(InteractionAreaManager manager)
        {
            this.manager = manager;
        }
        public override void Enter()
        { }

        public override void Exit()
        { }

        public override void Tick()
        { }
    }
}
