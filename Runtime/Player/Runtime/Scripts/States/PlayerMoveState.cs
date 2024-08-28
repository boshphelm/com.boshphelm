using System.Collections;
using System.Collections.Generic;
using Boshphelm.Utility;
using UnityEngine;
using UnityEngine.AI;

namespace Boshphelm.Player
{
    public class PlayerMoveState : PlayerBaseState
    {
        private readonly NavMeshMover _navMeshMover;

        private System.Action OnReachedToDestination = () => { };

        public PlayerMoveState(PlayerStateMachine playerStateMachine, NavMeshAgent navMeshAgent, Animator animator, Vector3 destination, float movementSpeed, System.Action onReachedToDestination) : base(playerStateMachine)
        {
            _navMeshMover = new NavMeshMover(navMeshAgent, animator, destination, movementSpeed, OnNavMeshMoverReachedToDestination);
            OnReachedToDestination += onReachedToDestination;
        }

        private void OnNavMeshMoverReachedToDestination()
        {
            OnReachedToDestination = () => { };
            OnReachedToDestination.Invoke();
        }

        public override void Enter()
        {
            _navMeshMover.Enter();
        }
        public override void Exit()
        {
            _navMeshMover.Exit();
        }
        public override void Tick()
        {
            _navMeshMover.Tick();
        }
        //public override string GetName() => "Move";

    }
}
