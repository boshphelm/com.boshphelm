using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Boshphelm.Panel
{
    public class GamePanelController : MonoBehaviour
    {
        [Header("Panels")] 
        [SerializeField] private CompletePanelController _completePanelController;
        [SerializeField] private FailPanelController _failPanelController;
        [SerializeField] private IngamePanelController _inGamePanelController; 
        [SerializeField] private MenuPanelController _menuPanelController;

        public void OpenCompletePanel()
        {
            CloseFailPanel(); 
            CloseInGamePanel();
            _completePanelController.Open();
        }

        public void CloseCompletePanel()
        {
            _completePanelController.Close();
        }

        public void OpenFailPanel()
        {
            CloseInGamePanel();
            CloseCompletePanel(); 
            _failPanelController.Open();
        }

        public void CloseFailPanel()
        {
            _failPanelController.Close();
        } 
        public void OpenInGamePanel()
        {
            _inGamePanelController.Open();
        }

        public void CloseInGamePanel()
        {
            _inGamePanelController.Close();
        }

        public void OpenMenuPanel()
        {
            _menuPanelController.Open();
        }

        public void CloseMenuPanel()
        {
            _menuPanelController.Close();
        }

        public void ActivateLevelStart()
        { 
            OpenMenuPanel();
            CloseInGamePanel();
        }
 
    }
}
