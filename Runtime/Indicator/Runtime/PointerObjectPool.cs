using UnityEngine;
using System.Collections.Generic;

namespace OffScreenIndicator
{
    public class PointerObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _pooledObject;
        [SerializeField] private int _pooledAmount = 20;

        public static PointerObjectPool Instance;

        private List<PointerIndicator> _pooledObjects;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializePool();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializePool()
        {
            _pooledObjects = new List<PointerIndicator>();
            for (int i = 0; i < _pooledAmount; i++)
            {
                CreatePooledObject();
            }
        }

        private void CreatePooledObject()
        {
            var obj = Instantiate(_pooledObject, transform);
            obj.SetActive(false);
            _pooledObjects.Add(obj.GetComponent<PointerIndicator>());
        }

        public PointerIndicator GetPooledObject()
        {
            for (int i = 0; i < _pooledObjects.Count; i++)
            {
                if (!_pooledObjects[i].gameObject.activeInHierarchy)
                {
                    return _pooledObjects[i];
                }
            }

            CreatePooledObject();
            return _pooledObjects[^1];
        }
    }
}
