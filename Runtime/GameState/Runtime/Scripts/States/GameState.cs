using Boshphelm.StateMachines;

namespace Boshphelm.GameStateSystem
{
    public abstract class GameState : State
    {
        protected GameStateManager StateManager { get; private set; }
        
        public GameState(GameStateManager manager)
        {
            StateManager = manager;
        } 
    }
}
