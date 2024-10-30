using UnityEngine;
using Boshphelm.Utility;
using Sirenix.OdinInspector;

namespace Boshphelm.Panel
{
    [CreateAssetMenu(fileName = "Game Panel Type", menuName = "Boshphelm/Panel/Game Panel Type")]
    public class GamePanelTypeSO : ScriptableObject
    {
        [SerializeField] private SerializableGuid _id = SerializableGuid.NewGuid();   
        public SerializableGuid Id => _id;  

        [Button(ButtonSizes.Large)]
        [GUIColor(0.4f, 0.8f, 1)]
        private void AssignNewGuid()
        {
            _id = SerializableGuid.NewGuid();
        }
    }
}
