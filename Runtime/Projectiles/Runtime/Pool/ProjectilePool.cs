using System.Collections;
using System.Collections.Generic;
using Boshphelm.Utility;
using UnityEngine;
using UnityEngine.Pool;

namespace Boshphelm.Projectiles
{
    public class ProjectilePool : MonoBehaviour, IProjectilePool
    {
        [SerializeField] private List<ProjectilePoolItem> _poolItems;

        private Dictionary<SerializableGuid, ObjectPool<IProjectile>> _poolDictionary;

        public void Initialize()
        {
            InitializePools();
        }

        private void InitializePools()
        {
            _poolDictionary = new Dictionary<SerializableGuid, ObjectPool<IProjectile>>();

            foreach (var item in _poolItems)
            {
                var objectPool = new ObjectPool<IProjectile>(
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
        }

        private IProjectile CreatePooledItem(ProjectilePoolItem item)
        {
            var obj = Instantiate(item.Prefab, transform);
            var projectile = obj.GetComponent<IProjectile>();
            projectile.Initialize(this);
            return projectile;
        }

        private void OnTakeFromPool(IProjectile projectile)
        {
            projectile.GameObject.SetActive(true);
        }

        private void OnReturnedToPool(IProjectile projectile)
        {
            projectile.GameObject.SetActive(false);
        }

        private void OnDestroyPoolObject(IProjectile projectile)
        {
            Destroy(projectile.GameObject);
        }

        public IProjectile SpawnProjectile(ProjectileType type, Vector3 position, Quaternion rotation)
        {
            if (!_poolDictionary.TryGetValue(type.Id, out var pool))
            {
                Debug.LogWarning($"Pool for projectile type {type.name} doesn't exist.");
                return null;
            }

            var projectile = pool.Get();
            projectile.Launch(position, rotation);
            return projectile;
        }

        public void ReturnToPool(IProjectile projectile)
        {
            if (_poolDictionary.TryGetValue(projectile.ProjectileType.Id, out var pool))
            {
                pool.Release(projectile);
            }
            else
            {
                Debug.LogWarning($"Pool for projectile type {projectile.ProjectileType.name} doesn't exist.");
            }
        }
    }
}
