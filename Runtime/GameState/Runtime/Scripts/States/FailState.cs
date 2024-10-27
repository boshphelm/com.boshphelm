using Boshphelm.Panel;
using UnityEngine;

namespace Boshphelm.GameStateSystem
{
    public class FailState : GameState
    {
        private readonly IGamePanelService _panelService;

        public FailState(GameStateManager manager, IGamePanelService panelService) 
            : base(manager)
        {
            _panelService = panelService;
        }

        public override void Enter()
        {
            Time.timeScale = 0f;
            _panelService.ClosePanel(GamePanelType.InGame);
            _panelService.OpenPanel(GamePanelType.Fail);
        }

        public override void Exit()
        {
            _panelService.ClosePanel(GamePanelType.Fail);
        }

        public override void Tick()
        { 
        }
    }
}
