using System;
using Boshphelm.Projectiles;
using UnityEngine;

namespace Boshphelm.OLD.Projectiles
{
    public class LockOnProjectile : MonoBehaviour, IPooledProjectile
    {
        [SerializeField] private float _speed = 10f;

        private Transform _myTransform;
        private GameObject _myGameObject;
        private Transform _targetTransform;

        public Transform Transform => _myTransform;
        public GameObject GameObject => _myGameObject;
        public Transform TargetTransform => _targetTransform;
        public ProjectilePool Pool { get; set; }

        public ProjectileType ProjectileType { get; set; }
        public int PenetrationCount { get; protected set; }
        public event Action<IPooledProjectile> OnProjectileHit = _ => { };

        private bool _released;

        private Vector3 _diff;
        private Vector3 _displacement;

        public void Initialize()
        {
            _myTransform = transform;
            _myGameObject = gameObject;
        }

        public void Launch(Transform targetTransform, int penetrationCount = 0)
        {
            _targetTransform = targetTransform;
            PenetrationCount = penetrationCount;
            _released = false;

            SetInitialRotation();
        }

        private void SetInitialRotation()
        {
            var myPosition = _myTransform.position;
            var targetPosition = _targetTransform.position;
            var initialDiff = targetPosition - myPosition;

            var direction = initialDiff.normalized;

            float angle = 90 - Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        protected virtual void Update()
        {
            if (_targetTransform == null)
            {
                ReturnToPool();
                return;
            }

            MoveToTarget();

            if (HasReachedTheTarget())
            {
                HitTarget();
            }
        }

        private void MoveToTarget()
        {
            var myPosition = _myTransform.position;
            var targetPosition = _targetTransform.position;

            _diff = targetPosition - myPosition;
            var direction = _diff.normalized;

            _displacement = direction * _speed * Time.deltaTime;
            myPosition += _displacement;
            _myTransform.position = myPosition;
        }

        private bool HasReachedTheTarget() => _diff.sqrMagnitude < 0.2f;

        protected virtual void HitTarget()
        {
            OnProjectileHit.Invoke(this);
            PenetrationCount--;
            if (PenetrationCount < 0)
            {
                ReturnToPool();
            }
        }

        public void OnObjectSpawn()
        {

        }
        public virtual void ReturnToPool()
        {
            if (_released) return;

            if (Pool != null && ProjectileType != null)
            {
                Pool.ReturnToPool(ProjectileType, this);
                _released = true;
            }
        }
    }
}
