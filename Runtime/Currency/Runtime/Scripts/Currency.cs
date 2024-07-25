using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Currencies
{
    [System.Serializable]
    public class Currency
    {
        [field: SerializeField] public SerializableGuid Id;
        [field: SerializeField] public SerializableGuid detailsId;
        public int quantity;
        public CurrencyDetail detail; 

        public Currency(CurrencyDetail detail, int quantity = 1)
        {
            Id = SerializableGuid.NewGuid();

            this.detail = detail;
            detailsId = detail.Id;
            //Debug.Log("NEW CURRENCY ID : " + Id.ToHexString() + ", DETAIL ID : " + detailsId.ToHexString(), details);

            this.quantity = quantity;
        }
    }
}