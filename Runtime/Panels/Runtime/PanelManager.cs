using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.Panel
{
    public class PanelManager : MonoBehaviour
    {
        [SerializeField] private PanelBase[] _panelsInScene;

        private readonly Dictionary<PanelType, IPanel> _panels = new Dictionary<PanelType, IPanel>();

        private void Awake()
        {
            InitializePanels();
        }

        private void InitializePanels()
        {
            foreach (var panel in _panelsInScene)
            {
                _panels[panel.Type] = panel;
            }
        }

        public void OpenPanel(PanelType panelType)
        {
            if (_panels.TryGetValue(panelType, out var panel))
            {
                if (!panel.IsOpen)
                {
                    panel.Open();
                }
            }
            else
            {
                Debug.LogWarning($"Panel of type {panelType} not found.");
            }
        }

        public void OpenPanels(params PanelType[] panelTypes)
        {
            foreach (var panelType in panelTypes)
            {
                OpenPanel(panelType);
            }
        }

        public void ClosePanel(PanelType panelType)
        {
            if (_panels.TryGetValue(panelType, out var panel))
            {
                if (panel.IsOpen)
                {
                    panel.Close();
                }
            }
        }

        public void ClosePanels(params PanelType[] panelTypes)
        {
            foreach (var panelType in panelTypes)
            {
                ClosePanel(panelType);
            }
        }

        public void CloseAllPanels()
        {
            foreach (var panel in _panels.Values)
            {
                if (panel.IsOpen)
                {
                    panel.Close();
                }
            }
        }

        public bool IsPanelOpen(PanelType panelType) => _panels.TryGetValue(panelType, out var panel) && panel.IsOpen;
    }
}
