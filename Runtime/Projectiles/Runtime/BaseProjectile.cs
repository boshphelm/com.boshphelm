using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.Projectiles
{
    public abstract class BaseProjectile : MonoBehaviour, IProjectile
    {
        [SerializeField] private ProjectileType _projectileType;

        private readonly List<IProjectileObserver> _observers = new List<IProjectileObserver>();

        protected IProjectilePool pool;
        protected Transform myTransform;
        protected GameObject myGameObject;
        protected bool isActive;

        public ProjectileType ProjectileType => _projectileType;
        public GameObject GameObject => myGameObject;
        public Transform Transform => myTransform;

        public virtual void Initialize(IProjectilePool projectilePool)
        {
            pool = projectilePool;
            myTransform = transform;
            myGameObject = gameObject;
        }

        public abstract void Launch(Vector3 position, Quaternion rotation);
        public abstract void Update();

        public virtual void OnHit(GameObject target)
        {
            NotifyObservers(target);
            ReturnToPool();
        }

        public virtual void ReturnToPool()
        {
            if (!isActive) return;

            isActive = false;
            pool.ReturnToPool(this);
        }

        public void AddObserver(IProjectileObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IProjectileObserver observer)
        {
            _observers.Remove(observer);
        }

        protected void NotifyObservers(GameObject target)
        {
            foreach (var observer in _observers)
            {
                observer.OnProjectileHit(this, target);
            }
        }
    }
}
