using Boshphelm.Panel;
using UnityEngine;

namespace Boshphelm.GameStateSystem
{
    // 1. State tanımları
    public class MenuState : GameState
    {
        private readonly IGamePanelService _panelService;

        public MenuState(GameStateManager manager, IGamePanelService panelService) 
            : base(manager)
        {
            _panelService = panelService;
        }

        public override void Enter()
        {
            Time.timeScale = 0f;
            _panelService.CloseAllPanels();
            _panelService.OpenPanel(GamePanelType.Menu);
        }

        public override void Exit()
        {
            _panelService.ClosePanel(GamePanelType.Menu);
        }

        public override void Tick()
        { 
        }
    } 
}
