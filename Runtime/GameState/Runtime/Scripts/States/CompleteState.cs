using Boshphelm.Panel;
using UnityEngine;

namespace Boshphelm.GameStateSystem
{
    public class CompleteState : GameState
    {
        private readonly IGamePanelService _panelService;

        public CompleteState(GameStateManager manager, IGamePanelService panelService) 
            : base(manager)
        {
            _panelService = panelService;
        }

        public override void Enter()
        {
            Time.timeScale = 0f;
            _panelService.ClosePanel(GamePanelType.InGame);
            _panelService.OpenPanel(GamePanelType.Complete);
        }

        public override void Exit()
        {
            _panelService.ClosePanel(GamePanelType.Complete);
        }

        public override void Tick()
        { 
        }
    }
}
