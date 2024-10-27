using Boshphelm.Panel;
using UnityEngine;

namespace Boshphelm.GameStateSystem
{
    public class PlayState : GameState
    {
        private readonly IGamePanelService _panelService;

        public PlayState(GameStateManager manager, IGamePanelService panelService) 
            : base(manager)
        {
            _panelService = panelService;
        }

        public override void Enter()
        {
            Time.timeScale = 1f;
            _panelService.CloseAllPanels();
            _panelService.OpenPanel(GamePanelType.InGame);
        }

        public override void Exit()
        {
            _panelService.ClosePanel(GamePanelType.InGame);
        }

        public override void Tick()
        { 
        }
    }
}
