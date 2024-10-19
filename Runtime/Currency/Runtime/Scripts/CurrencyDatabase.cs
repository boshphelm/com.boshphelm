using System.Collections.Generic;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Currencies
{
    public static class CurrencyDatabase
    {
        private static Dictionary<SerializableGuid, CurrencyDataSO> _currencyDetailsDictionary;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Initialize()
        {
            _currencyDetailsDictionary = new Dictionary<SerializableGuid, CurrencyDataSO>();

            var currencyDataSos = Resources.LoadAll<CurrencyDataSO>("");
            foreach (var currencyDataSo in currencyDataSos)
            {
                _currencyDetailsDictionary.Add(currencyDataSo.Id, currencyDataSo);
            }
        }

        public static CurrencyDataSO GetCurrencyDataById(SerializableGuid id)
        {
            try
            {
                return _currencyDetailsDictionary[id];
            }
            catch
            {
                Debug.LogError($"Cannot find currency with id : {id.ToHexString()}");
                return null;
            }
        }
    }
}
