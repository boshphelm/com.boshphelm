using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.Stats
{
    public abstract class StatValueWriter : MonoBehaviour, IStatListener
    {
        [SerializeField] private List<StatListenData> _statListenDatas;

        private IStatContainer _statContainer;

        private void Awake()
        {
            _statContainer = GetComponent<IStatContainer>();
        }

        private void Start()
        {
            if (_statContainer == null) return;
            foreach (var statListenData in _statListenDatas)
            {
                var stat = _statContainer.GetStatByStatType(statListenData.StatType);
                stat?.RegisterValueListener(this);
            }
        }

        public void OnBaseValueChanged(StatType statType, float newBaseValue)
        {
        }

        public void OnTotalValueChanged(StatType statType, float newTotalValue)
        {
            int foundIndex = FindStatListenDataIndexByStatType(statType);
            if (foundIndex == -1) return;

            _statListenDatas[foundIndex].Value = newTotalValue;
        }

        private int FindStatListenDataIndexByStatType(StatType statType)
        {
            for (int i = 0; i < _statListenDatas.Count; i++)
            {
                if (_statListenDatas[i].StatType.Id == statType.Id) return i;
            }

            return -1;
        }

        [System.Serializable]
        public class StatListenData
        {
            public StatType StatType;
            public float Value;
        }
    }
}
