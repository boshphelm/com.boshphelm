using UnityEngine;
using System.Collections.Generic;
using Boshphelm.Utility;

namespace Boshphelm.Panel
{
    [CreateAssetMenu(fileName = "Panel Type Database", menuName = "Boshphelm/Panel/Panel Type Database")]
    public class GamePanelTypeDatabase : ScriptableObject
    {
        [SerializeField] private GamePanelTypeSO[] _panelTypes;
        
        private static Dictionary<SerializableGuid, GamePanelTypeSO> _panelTypeDict;

        private void OnEnable()
        {
            InitializeDictionary();
        }

        private void InitializeDictionary()
        {
            _panelTypeDict = new Dictionary<SerializableGuid, GamePanelTypeSO>();
            foreach (var panelType in _panelTypes)
            {
                if (panelType != null)
                {
                    _panelTypeDict[panelType.Id] = panelType;
                }
            }
        }

        public GamePanelTypeSO GetPanelType(SerializableGuid id)
        {
            if (_panelTypeDict == null)
            {
                InitializeDictionary();
            }

            return _panelTypeDict.TryGetValue(id, out var panelType) ? panelType : null;
        }

        public GamePanelTypeSO[] GetAllPanelTypes() => _panelTypes;
    }
}
