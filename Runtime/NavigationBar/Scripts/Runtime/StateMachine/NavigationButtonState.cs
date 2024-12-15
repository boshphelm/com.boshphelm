using Boshphelm.StateMachines;

namespace Boshphelm.NavigationBars
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
