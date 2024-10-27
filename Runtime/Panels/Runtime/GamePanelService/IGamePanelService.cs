using System;

namespace Boshphelm.Panel
{
    public interface IGamePanelService
    {
        void OpenPanel(GamePanelType panelType);
        void ClosePanel(GamePanelType panelType);
        void TogglePanel(GamePanelType panelType);
        void CloseAllPanels();
        bool IsPanelOpen(GamePanelType panelType);

        event Action<GamePanelType, bool> OnPanelStateChanged;
    }
}