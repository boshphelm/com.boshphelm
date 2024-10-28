using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq; 

namespace Boshphelm.Panel
{
    public class GamePanelService : MonoBehaviour, IGamePanelService
    {
        [SerializeField] private PanelConfig[] _panelConfigs; 
        
        private Dictionary<GamePanelType, PanelBase> _panels;
        private HashSet<GamePanelType> _activePanels;
        private Dictionary<GamePanelType, List<GamePanelType>> _openingPanels; 
        public event Action<GamePanelType, bool> OnPanelStateChanged;
        
        private void Awake()
        {
            InitializePanels();
        }

        private void InitializePanels()
        {
            _panels = new Dictionary<GamePanelType, PanelBase>();
            _activePanels = new HashSet<GamePanelType>();
            _openingPanels = new Dictionary<GamePanelType, List<GamePanelType>>();

            foreach (var config in _panelConfigs)
            {
                if (config.Panel != null)
                {
                    _panels[config.Type] = config.Panel;
                }
            }
        }

        public void OpenPanel(GamePanelType panelType)
        {
            if (!_panels.TryGetValue(panelType, out var panel))
            {
                Debug.LogWarning($"Panel not found: {panelType}");
                return;
            }

            if (IsCircularDependency(panelType))
            {
                Debug.LogError($"Circular dependency detected while opening panel: {panelType}");
                return;
            }

            var config = GetPanelConfig(panelType);
            if (config == null) return;
 
            else if (config.IncompatiblePanels != null)
            {
                foreach (var incompatibleType in config.IncompatiblePanels)
                {
                    ClosePanel(incompatibleType);
                }
            }

            BeginPanelOpening(panelType);

            panel.Open();
            _activePanels.Add(panelType);
            OnPanelStateChanged?.Invoke(panelType, true);

            if (config.LinkedPanels != null)
            {
                foreach (var linkedType in config.LinkedPanels)
                {
                    if (!_activePanels.Contains(linkedType))
                    {
                        OpenPanel(linkedType);
                    }
                }
            }

            EndPanelOpening(panelType);
        }

        public void ClosePanel(GamePanelType panelType)
        {
            if (!_panels.TryGetValue(panelType, out var panel))
            {
                return;
            }

            var config = GetPanelConfig(panelType);
            if (config == null) return;

            if (config.LinkedPanels != null)
            {
                foreach (var linkedType in config.LinkedPanels)
                {
                    if (_activePanels.Contains(linkedType))
                    {
                        ClosePanel(linkedType);
                    }
                }
            }

            panel.Close();
            _activePanels.Remove(panelType);
            OnPanelStateChanged?.Invoke(panelType, false);
        }

        public void CloseAllPanels()
        {
            foreach (var panelType in _activePanels.ToList())
            {
                ClosePanel(panelType);
            }
        }

        public bool IsPanelOpen(GamePanelType panelType)
        {
            return _activePanels.Contains(panelType);
        }

        public void TogglePanel(GamePanelType panelType)
        {
            if (IsPanelOpen(panelType))
                ClosePanel(panelType);
            else
                OpenPanel(panelType);
        }

        private bool IsCircularDependency(GamePanelType panelType)
        {
            if (!_openingPanels.ContainsKey(panelType))
            {
                return false;
            }

            var config = GetPanelConfig(panelType);
            if (config?.LinkedPanels == null) return false;

            foreach (var linkedType in config.LinkedPanels)
            {
                if (_openingPanels[panelType].Contains(linkedType))
                {
                    return true;
                }
            }

            return false;
        }

        private void BeginPanelOpening(GamePanelType panelType)
        {
            if (!_openingPanels.ContainsKey(panelType))
            {
                _openingPanels[panelType] = new List<GamePanelType>();
            }

            var config = GetPanelConfig(panelType);
            if (config?.LinkedPanels != null)
            {
                _openingPanels[panelType].AddRange(config.LinkedPanels);
            }
        }

        private void EndPanelOpening(GamePanelType panelType)
        {
            _openingPanels.Remove(panelType);
        }

        private PanelConfig GetPanelConfig(GamePanelType type)
        {
            return _panelConfigs.FirstOrDefault(c => c.Type == type);
        }
    }
}