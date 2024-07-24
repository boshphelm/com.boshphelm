using Boshphelm.StateMachines;
using UnityEngine;

namespace Boshphelm.AreaInteractionSystem
{
    public class SwitchToInactiveStateSubState : BaseInteractionSubState
    {
        public SwitchToInactiveStateSubState(InteractionAreaManager manager) : base(manager) { }

        public override void Enter()
        {
            manager.SwitchState(new InactiveState(manager));
        }

        public override void Tick() { }

        public override void Exit() { }

    }
}
