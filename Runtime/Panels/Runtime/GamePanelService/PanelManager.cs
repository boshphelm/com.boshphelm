using System.Collections.Generic;
using Boshphelm.Utility; 

namespace Boshphelm.Panel
{
    public class PanelManager
    {
        private readonly Dictionary<SerializableGuid, PanelBase> _panels;
        private readonly HashSet<SerializableGuid> _activePanels;
        private readonly GamePanelTypeDatabase _database;

        public PanelManager(GamePanelTypeDatabase database)
        {
            _panels = new Dictionary<SerializableGuid, PanelBase>();
            _activePanels = new HashSet<SerializableGuid>();
            _database = database;
        }

        public void RegisterPanel(GamePanelTypeSO type, PanelBase panel)
        {
            if (type != null && panel != null)
            {
                _panels[type.Id] = panel;
            }
        }

        public bool TryGetPanel(SerializableGuid typeId, out PanelBase panel)
        {
            return _panels.TryGetValue(typeId, out panel);
        }

        public void MarkPanelAsActive(SerializableGuid typeId)
        {
            _activePanels.Add(typeId);
        }

        public void MarkPanelAsInactive(SerializableGuid typeId)
        {
            _activePanels.Remove(typeId);
        }

        public bool IsPanelActive(SerializableGuid typeId)
        {
            return _activePanels.Contains(typeId);
        }

        public IEnumerable<SerializableGuid> GetActivePanels()
        {
            return _activePanels;
        }
    }
}
