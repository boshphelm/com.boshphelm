using Boshphelm.Panel;
using UnityEngine;

namespace Boshphelm.GameStateSystem
{ 
    // 3. State'leri dinleyen UI Manager örneği
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField] private GameStateManager _stateManager;
        [SerializeField] private GamePanelService _panelService;

        private void Start()
        {
            _stateManager.OnStateChanged += HandleStateChanged;
            _panelService.OnPanelStateChanged += HandlePanelStateChanged;
        }

        private void HandleStateChanged(GameState newState)
        {
            Debug.Log($"Game state changed to: {newState.GetType().Name}");
        }

        private void HandlePanelStateChanged(GamePanelType panelType, bool isOpen)
        {
            Debug.Log($"Panel {panelType} is now {(isOpen ? "open" : "closed")}");
        }

        // UI Button handlers
        public void OnPlayButtonClicked()
        {
            _stateManager.StartGame();
        }

        public void OnMainMenuButtonClicked()
        {
            _stateManager.GoToMenu();
        }

        public void OnPauseButtonClicked()
        {
            // İhtiyaca göre pause state ekleyebilirsiniz
        }

        public void OnRestartButtonClicked()
        {
            _stateManager.StartGame();
        }

        private void OnDestroy()
        {
            if (_stateManager != null)
                _stateManager.OnStateChanged -= HandleStateChanged;
            
            if (_panelService != null)
                _panelService.OnPanelStateChanged -= HandlePanelStateChanged;
        }
    }
}
