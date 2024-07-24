using Boshphelm.StateMachines;

namespace Boshphelm.Enemy
{
    public abstract class EnemyBaseState : State
    {
        private EnemyStateMachine _enemyStateMachine;

        protected EnemyBaseState(EnemyStateMachine enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
        }
    }
}
