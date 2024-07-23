using System;
using UnityEngine;
using UnityEngine.AI;

namespace Boshphelm.Utility
{
    public class NavMeshMover
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly Vector3 _destination;
        private readonly float _speed;
        private readonly Transform _navMeshTransform;

        private bool _arrive;
        private float _initialSpeed = 1f;

        private Action _onReachedToDestination;

        private const float AnimationMoveSpeedRate = .25f;

        private static readonly int MoveHash = Animator.StringToHash("Move");
        private static readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");

        public NavMeshMover(NavMeshAgent navMeshAgent, Animator animator, Vector3 destination, float speed, Action onReachedToDestination)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _destination = destination;
            _speed = speed;
            _onReachedToDestination = onReachedToDestination;

            _navMeshTransform = _navMeshAgent.transform;
        }


        public void Enter()
        {
            _navMeshAgent.enabled = true;
            _initialSpeed = _navMeshAgent.speed;
            _navMeshAgent.speed = _speed;

            float animationSpeed = _speed * AnimationMoveSpeedRate;
            _animator.SetFloat(MoveSpeedHash, animationSpeed);

            _navMeshAgent.SetDestination(_destination);

            _animator.SetBool(MoveHash, true);
        }

        public void Exit()
        {
            _onReachedToDestination = null;

            _navMeshAgent.speed = _initialSpeed;

            float initialAnimationSpeed = _initialSpeed * AnimationMoveSpeedRate;
            _animator.SetFloat(MoveSpeedHash, initialAnimationSpeed);

            _navMeshAgent.enabled = false;

            _animator.SetBool(MoveHash, false);
        }

        public void Tick()
        {
            if (_arrive) return;

            Vector3 localVelocity = _navMeshTransform.InverseTransformDirection(_navMeshAgent.velocity);
            float speed = localVelocity.z;

            //_animator.SetFloat(Speed, speed);

            if (_navMeshAgent.pathPending) return;
            if (!(_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)) return;
            if (_navMeshAgent.hasPath && _navMeshAgent.velocity.sqrMagnitude != 0f) return;

            _arrive = true;
            _onReachedToDestination?.Invoke();
        }
    }
}