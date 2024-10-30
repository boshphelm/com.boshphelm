using System.Collections.Generic;
using System.Linq;
using Boshphelm.Utility;

namespace Boshphelm.Panel
{
    public class PanelDependencyChecker
    {
        private readonly Dictionary<SerializableGuid, List<SerializableGuid>> _openingPanels;

        public PanelDependencyChecker()
        {
            _openingPanels = new Dictionary<SerializableGuid, List<SerializableGuid>>();
        }

        public bool HasCircularDependency(SerializableGuid panelId, PanelConfig config)
        {
            if (!_openingPanels.ContainsKey(panelId))
                return false;

            if (config?.LinkedPanels == null) 
                return false;

            return config.LinkedPanels.Any(linkedType => 
                _openingPanels[panelId].Contains(linkedType.Id));
        }

        public void BeginPanelOpening(SerializableGuid panelId, PanelConfig config)
        {
            if (!_openingPanels.ContainsKey(panelId))
            {
                _openingPanels[panelId] = new List<SerializableGuid>();
            }

            if (config?.LinkedPanels != null)
            {
                _openingPanels[panelId].AddRange(config.LinkedPanels.Select(p => p.Id));
            }
        }

        public void EndPanelOpening(SerializableGuid panelId)
        {
            _openingPanels.Remove(panelId);
        }
    }
}
