using UnityEngine;
using System.Collections.Generic;

namespace OffScreenIndicator
{
    public class PointerObjectPool : MonoBehaviour
    {
        public static PointerObjectPool Instance;
        [SerializeField] private GameObject pooledObject;
        [SerializeField] private int pooledAmount = 20;
        private List<PointerIndicator> pooledObjects;

        private void Awake()
        {
            Instance = this;

            pooledObjects = new List<PointerIndicator>();
            for (int i = 0; i < pooledAmount; i++)
            {
                GameObject obj = Instantiate(pooledObject, transform);
                obj.SetActive(false);
                pooledObjects.Add(obj.GetComponent<PointerIndicator>());
            }
        }

        public PointerIndicator GetPooledObject()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].gameObject.activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }

            GameObject obj = Instantiate(pooledObject, transform);
            obj.SetActive(false);
            pooledObjects.Add(obj.GetComponent<PointerIndicator>());
            return pooledObjects[pooledObjects.Count - 1];
        }
    }
}
