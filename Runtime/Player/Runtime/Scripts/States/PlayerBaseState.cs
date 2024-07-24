using Boshphelm.StateMachines;

namespace Boshphelm.Player
{
    public abstract class PlayerBaseState : State
    {
        protected PlayerStateMachine playerStateMachine;

        protected PlayerBaseState(PlayerStateMachine playerStateMachine)
        {
            this.playerStateMachine = playerStateMachine;
        }
    }
}
