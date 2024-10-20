using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Projectiles
{
    public class DirectionalProjectile : BaseProjectile, IDirectional
    {
        [SerializeField] private float _speed = 15f;
        [SerializeField] private float _maxLifetime = 5f;
        private Vector3 _direction;
        private float _lifetime;

        public void SetDirection(Vector3 direction)
        {
            _direction = direction.normalized;
        }

        public override void Launch(Vector3 position, Quaternion rotation)
        {
            myTransform.SetPositionAndRotation(position, rotation);
            _lifetime = 0f;
            isActive = true;
            gameObject.SetActive(true);
        }

        public override void Update()
        {
            if (!isActive) return;

            Move();
            LifeTimeCheck();
        }

        private void Move()
        {
            myTransform.position += _direction * _speed * Time.deltaTime;
            _lifetime += Time.deltaTime;
        }

        private void LifeTimeCheck()
        {
            if (_lifetime >= _maxLifetime)
            {
                ReturnToPool();
            }
        }
    }
}
