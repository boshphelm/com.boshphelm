using System;
using UnityEngine;

namespace Boshphelm.Panel
{
    [Serializable]
    public class PanelConfig
    {
        [Header("Core Settings")]
        public GamePanelTypeSO Type;
        public PanelBase Panel;
        
        [Header("Dependencies")]
        [Tooltip("Panels that will be opened automatically when this panel opens")]
        public GamePanelTypeSO[] LinkedPanels;
        
        [Tooltip("Panels that cannot be open at the same time as this panel")]
        public GamePanelTypeSO[] IncompatiblePanels;
    }
}
