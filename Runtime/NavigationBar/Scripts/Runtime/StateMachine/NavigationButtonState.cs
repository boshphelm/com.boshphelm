using Boshphelm.StateMachines;

namespace Boshphelm.NavigationBar
{
    public abstract class NavigationButtonState : State
    {
        protected NavigationButtonStateMachine stateMachine;

        protected NavigationButtonState(NavigationButtonStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    }
}
