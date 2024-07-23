using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Currencies
{ 
    [CreateAssetMenu(fileName = "New Currency Detail", menuName = "Currency/CurrencyData"), System.Serializable]
    public class CurrencyData : ScriptableObject
    {
        public SerializableGuid Id => detail.Id;
        
        public string displayName;

        public Sprite Icon;

        public string Description;
      
        public CurrencyDetail detail;
 
        public Currency Create(int quantity) => new Currency(detail, quantity);
    }

    [System.Serializable]
    public class CurrencyDetail
    {  
        public SerializableGuid Id = SerializableGuid.NewGuid(); 
    }
}