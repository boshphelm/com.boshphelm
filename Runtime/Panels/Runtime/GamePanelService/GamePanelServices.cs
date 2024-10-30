using UnityEngine;
using System;
using System.Linq;

namespace Boshphelm.Panel
{
   public class GamePanelService : MonoBehaviour, IGamePanelService
    {
        [SerializeField] private GamePanelTypeDatabase _panelTypeDatabase;
        [SerializeField] private PanelConfig[] _panelSetups;

        private PanelManager _panelManager;
        private PanelDependencyChecker _dependencyChecker;
        
        public event Action<GamePanelTypeSO, bool> OnPanelStateChanged;

        private void Awake()
        {
            InitializeServices();
            RegisterPanels();
        }

        private void InitializeServices()
        {
            _panelManager = new PanelManager(_panelTypeDatabase);
            _dependencyChecker = new PanelDependencyChecker();
        }

        private void RegisterPanels()
        {
            foreach (var config in _panelSetups)
            {
                _panelManager.RegisterPanel(config.Type, config.Panel);
            }
        }

        public void OpenPanel(GamePanelTypeSO panelType)
        {
            if (!_panelManager.TryGetPanel(panelType.Id, out var panel))
                return;

            var config = GetConfigSetup(panelType);
            if (config == null) return;

            if (_dependencyChecker.HasCircularDependency(panelType.Id, config))
                return;

            HandleIncompatiblePanels(config);
            OpenPanelWithDependencies(panelType, config, panel);
        }

        private void OpenPanelWithDependencies(GamePanelTypeSO panelType, PanelConfig config, PanelBase panel)
        {
            _dependencyChecker.BeginPanelOpening(panelType.Id, config);

            OpenSinglePanel(panelType, panel);
            OpenLinkedPanels(config);

            _dependencyChecker.EndPanelOpening(panelType.Id);
        }

        private void OpenSinglePanel(GamePanelTypeSO panelType, PanelBase panel)
        {
            panel.Open();
            _panelManager.MarkPanelAsActive(panelType.Id);
            OnPanelStateChanged?.Invoke(panelType, true);
        }

        private void OpenLinkedPanels(PanelConfig config)
        {
            if (config.LinkedPanels == null) return;

            foreach (var linkedType in config.LinkedPanels)
            {
                if (!_panelManager.IsPanelActive(linkedType.Id))
                {
                    OpenPanel(linkedType);
                }
            }
        }

        private void HandleIncompatiblePanels(PanelConfig config)
        {
            if (config.IncompatiblePanels == null) return;

            foreach (var incompatibleType in config.IncompatiblePanels)
            {
                ClosePanel(incompatibleType);
            }
        }

        public void ClosePanel(GamePanelTypeSO panelType)
        {
            if (!_panelManager.TryGetPanel(panelType.Id, out var panel))
                return;

            var config = GetConfigSetup(panelType);
            if (config == null) return;

            ClosePanelWithDependencies(panelType, config, panel);
        }

        private void ClosePanelWithDependencies(GamePanelTypeSO panelType, PanelConfig config, PanelBase panel)
        {
            CloseLinkedPanels(config);
            CloseSinglePanel(panelType, panel);
        }

        private void CloseSinglePanel(GamePanelTypeSO panelType, PanelBase panel)
        {
            panel.Close();
            _panelManager.MarkPanelAsInactive(panelType.Id);
            OnPanelStateChanged?.Invoke(panelType, false);
        }

        private void CloseLinkedPanels(PanelConfig config)
        {
            if (config.LinkedPanels == null) return;

            foreach (var linkedType in config.LinkedPanels)
            {
                if (_panelManager.IsPanelActive(linkedType.Id))
                {
                    ClosePanel(linkedType);
                }
            }
        }

        public void CloseAllPanels()
        {
            foreach (var panelId in _panelManager.GetActivePanels().ToList())
            {
                var panelType = _panelTypeDatabase.GetPanelType(panelId);
                if (panelType != null)
                {
                    ClosePanel(panelType);
                }
            }
        }

        public bool IsPanelOpen(GamePanelTypeSO panelType)
        {
            return _panelManager.IsPanelActive(panelType.Id);
        }

        public void TogglePanel(GamePanelTypeSO panelType)
        {
            if (IsPanelOpen(panelType))
                ClosePanel(panelType);
            else
                OpenPanel(panelType);
        }

        private PanelConfig GetConfigSetup(GamePanelTypeSO type)
        {
            return _panelSetups.FirstOrDefault(config => config.Type == type);
        }
    }
}
