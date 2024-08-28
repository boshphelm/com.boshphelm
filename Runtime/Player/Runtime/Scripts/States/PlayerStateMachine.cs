using System;
using System.Collections.Generic;
using Boshphelm.StateMachines;
using Codice.Client.BaseCommands;
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
            PlayerMoveState moveState = new PlayerMoveState(this, _navMeshAgent, _animator, destination, movementSpeed, onReachedToDestination);
            SwitchState(moveState);
        }

        private void AttackState()
        {
            PlayerAttackState attackState = new PlayerAttackState(this, _animator);
            SwitchState(attackState);
        }

    }
}
