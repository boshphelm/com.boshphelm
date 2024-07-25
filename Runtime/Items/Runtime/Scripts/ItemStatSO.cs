using UnityEngine;

namespace Boshphelm.Items
{
    [CreateAssetMenu(fileName = "ItemStatSO", menuName = "Boshphelm/Items/ItemStatSO"), System.Serializable]
    public class ItemStatSO : ScriptableObject
    {
        public Sprite Icon;
        public string DisplayName; 
    }
}