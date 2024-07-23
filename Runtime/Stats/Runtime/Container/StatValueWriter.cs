using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.Stats
{
    public abstract class StatValueWriter<T> : MonoBehaviour, IStatListener where T : StatType
    {
        [SerializeField] private List<StatListenData> statListenDatas;

        private IStatContainer<T> statContainer;

        private void Awake()
        {
            statContainer = GetComponent<IStatContainer<T>>();
        }

        private void Start()
        {
            if (statContainer == null) return;
            foreach (StatListenData statListenData in statListenDatas)
            {
                var stat = statContainer.GetStatByStatType(statListenData.StatType);
                stat?.RegisterValueListener(this);
            }
        }

        public void OnBaseValueChanged(StatType statType, float newBaseValue)
        {
        }

        public void OnTotalValueChanged(StatType statType, float newTotalValue)
        {
            int foundIndex = FindStatListenDataIndexByStatType((T)statType);
            if (foundIndex == -1) return;

            statListenDatas[foundIndex].Value = newTotalValue;
        }

        private int FindStatListenDataIndexByStatType(T statType)
        {
            for (int i = 0; i < statListenDatas.Count; i++)
            {
                if (statListenDatas[i].StatType == statType) return i;
            }

            return -1;
        }

        [System.Serializable]
        public class StatListenData
        {
            public T StatType;
            public float Value;
        }
    }
}