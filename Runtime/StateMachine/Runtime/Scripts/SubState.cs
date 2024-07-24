namespace Boshphelm.StateMachines
{
    public abstract class SubState
    {
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Tick();
    }
}