using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Boshphelm.Utility
{
    public class ParticlePool : PersistentSingleton<ParticlePool>
    {
        [System.Serializable]
        public class PoolItem
        {
            public ParticleType Type;
            public GameObject Prefab;
            public int DefaultCapacity = 10;
            public int MaxSize = 20;
        }

        public List<PoolItem> PoolItems;
        private Dictionary<SerializableGuid, ObjectPool<GameObject>> _poolDictionary;

        protected override void Awake()
        {
            _poolDictionary = new Dictionary<SerializableGuid, ObjectPool<GameObject>>();

            foreach (var item in PoolItems)
            {
                var objectPool = new ObjectPool<GameObject>(
                    () => CreatePooledItem(item),
                    OnTakeFromPool,
                    OnReturnedToPool,
                    OnDestroyPoolObject,
                    true,
                    item.DefaultCapacity,
                    item.MaxSize
                    );

                _poolDictionary.Add(item.Type.Id, objectPool);
            }

            base.Awake();
        }

        private GameObject CreatePooledItem(PoolItem item)
        {
            var obj = Instantiate(item.Prefab, transform);
            obj.SetActive(false);
            var pooledObject = obj.GetComponent<IPooledParticleObject>();
            if (pooledObject != null)
            {
                pooledObject.Initialize();
                pooledObject.Pool = this;
                pooledObject.ParticleType = item.Type;
            }
            return obj;
        }

        private void OnTakeFromPool(GameObject obj)
        {
            obj.SetActive(true);
        }

        private void OnReturnedToPool(GameObject obj)
        {
            obj.transform.parent = transform;
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(false);
        }

        private void OnDestroyPoolObject(GameObject obj)
        {
            Destroy(obj);
        }

        public IPooledParticleObject SpawnFromPoolInParent(ParticleType type, Transform parent)
        {
            if (!_poolDictionary.ContainsKey(type.Id))
            {
                Debug.LogWarning("Pool for particle type " + type.name + " doesn't exist.");
                return null;
            }

            var obj = _poolDictionary[type.Id].Get();
            obj.transform.parent = parent;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;

            var pooledObj = obj.GetComponent<IPooledParticleObject>();
            if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
            }

            return pooledObj;
        }

        public IPooledParticleObject SpawnFromPool(ParticleType type, Vector3 position, Quaternion rotation)
        {
            if (!_poolDictionary.ContainsKey(type.Id))
            {
                Debug.LogWarning("Pool for particle type " + type.name + " doesn't exist.");
                return null;
            }

            var obj = _poolDictionary[type.Id].Get();

            obj.transform.position = position;
            obj.transform.rotation = rotation;

            var pooledObj = obj.GetComponent<IPooledParticleObject>();
            if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
            }

            return pooledObj;
        }

        public IPooledParticleObject SpawnFromPool(ParticleType type, Vector3 position)
        {
            if (!_poolDictionary.ContainsKey(type.Id))
            {
                Debug.LogWarning("Pool for particle type " + type.name + " doesn't exist.");
                return null;
            }

            var obj = _poolDictionary[type.Id].Get();

            obj.transform.position = position;

            var pooledObj = obj.GetComponent<IPooledParticleObject>();
            if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
            }

            return pooledObj;
        }

        public void ReturnToPool(ParticleType type, GameObject obj)
        {
            if (!_poolDictionary.ContainsKey(type.Id))
            {
                Debug.LogWarning("Pool for particle type " + type.name + " doesn't exist.");
                return;
            }

            _poolDictionary[type.Id].Release(obj);
        }
    }
}
