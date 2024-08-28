using System;
using Boshphelm.StateMachines;
using UnityEngine;
using UnityEngine.AI;

namespace Boshphelm.Player
{
    public class PlayerStateMachine : StateMachine
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _navMeshAgent;


        private void MoveState(Vector3 destination, float movementSpeed, Action onReachedToDestination)
        {
            var moveState = new PlayerMoveState(this, _navMeshAgent, _animator, destination, movementSpeed, onReachedToDestination);
            SwitchState(moveState);
        }

        private void AttackState()
        {
            var attackState = new PlayerAttackState(this, _animator);
            SwitchState(attackState);
        }

    }
}
