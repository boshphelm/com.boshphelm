using System;

namespace Boshphelm.Panel
{
    public interface IGamePanelService
    {
        void OpenPanel(GamePanelTypeSO panelType);
        void ClosePanel(GamePanelTypeSO panelType);
        void TogglePanel(GamePanelTypeSO panelType);
        void CloseAllPanels();
        bool IsPanelOpen(GamePanelTypeSO panelType);

        event Action<GamePanelTypeSO, bool> OnPanelStateChanged;
    }
}