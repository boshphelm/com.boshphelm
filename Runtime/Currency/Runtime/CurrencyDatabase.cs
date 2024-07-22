using System.Collections.Generic;
using boshphelm.Utility;
using UnityEngine;

namespace boshphelm.Currencies
{
    public static class CurrencyDatabase
    {
        private static Dictionary<SerializableGuid, CurrencyDetail> currencyDetailsDictionary;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Initialize()
        {
        /*     currencyDetailsDictionary = new Dictionary<SerializableGuid, CurrencyDetail>();

            CurrencyDetail[] itemDetails = Resources.LoadAll<CurrencyDetail>("");
            foreach (CurrencyDetail itemDetail in itemDetails)
            {
                currencyDetailsDictionary.Add(itemDetail.Id, itemDetail);
            } */
        }

        public static CurrencyDetail GetDetailsById(SerializableGuid id)
        {
            try
            {
                return currencyDetailsDictionary[id];
            }
            catch
            {
                Debug.LogError($"Cannot find currency with id : {id.ToHexString()}");
                return null;
            }
        }
    }
}