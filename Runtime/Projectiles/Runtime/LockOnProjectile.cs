using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Projectiles
{
    public class LockOnProjectile : BaseProjectile, ITargetable
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _hitDistanceAt = .2f;
        private Transform _targetTransform;


        public void SetTarget(Transform target)
        {
            _targetTransform = target;
        }

        public override void Launch(Vector3 position, Quaternion rotation)
        {
            myTransform.SetPositionAndRotation(position, rotation);
            isActive = true;
            gameObject.SetActive(true);
        }

        public override void Update()
        {
            if (!isActive || _targetTransform == null)
            {
                ReturnToPool();
                return;
            }

            Move();
            DistanceCheck();
        }

        private void Move()
        {
            var myPosition = myTransform.position;
            var direction = (_targetTransform.position - myPosition).normalized;

            myPosition += direction * _speed * Time.deltaTime;
            myTransform.position = myPosition;
        }

        private void DistanceCheck()
        {
            float distance = myTransform.MagnitudeDistance(_targetTransform);
            if (distance < _hitDistanceAt)
            {
                OnHit(_targetTransform.gameObject);
            }
        }
    }
}
