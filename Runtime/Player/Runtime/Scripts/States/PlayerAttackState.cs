using UnityEngine;

namespace Boshphelm.Player
{
    public class PlayerAttackState : PlayerBaseState
    {
        private Animator _animator;
        public PlayerAttackState(PlayerStateMachine playerStateMachine, Animator animator) : base(playerStateMachine)
        {
            this._animator = animator;
        }

        public override void Enter()
        {

        }

        public override void Exit()
        {

        }

        public override void Tick()
        {

        }
        public override string GetName() => "Attack";
    }
}
