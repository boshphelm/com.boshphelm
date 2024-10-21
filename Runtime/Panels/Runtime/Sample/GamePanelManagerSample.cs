using UnityEngine;

namespace Boshphelm.Panel
{
    public class GamePanelManagerSample : MonoBehaviour
    {
        [SerializeField] private PanelManager _panelManager;

        public void OnLevelComplete()
        {
            _panelManager.OpenPanel(PanelType.Complete);
        }

        public void OnLevelFail()
        {
            _panelManager.OpenPanel(PanelType.Fail);
        }

        public void StartGame()
        {
            _panelManager.OpenPanels(PanelType.Ingame, PanelType.Inventory);
        }

        public void OpenSettings()
        {
            _panelManager.OpenPanel(PanelType.Settings);
        }

        public void CloseSettings()
        {
            _panelManager.ClosePanel(PanelType.Settings);
        }

        public void ReturnToMainMenu()
        {
            _panelManager.CloseAllPanels();
            _panelManager.OpenPanel(PanelType.MainMenu);
        }

        public void ToggleInventory()
        {
            if (_panelManager.IsPanelOpen(PanelType.Inventory))
            {
                _panelManager.ClosePanel(PanelType.Inventory);
            }
            else
            {
                _panelManager.OpenPanel(PanelType.Inventory);
            }
        }
    }
}
