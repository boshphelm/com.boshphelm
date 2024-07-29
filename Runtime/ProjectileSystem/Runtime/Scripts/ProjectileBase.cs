using System;
using UnityEngine;

namespace Boshphelm.ProjectileSystem
{
    public class ProjectileBase : MonoBehaviour
    {
        public float speed = 10f;
        public float lifeTime = 5f;

        public Action<Collider, ProjectileBase> OnHit;

        private Rigidbody rb;
        private float timer;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            timer = 0f;
        }

        private void FixedUpdate()
        {
            rb.velocity = transform.forward * speed;
            timer += Time.fixedDeltaTime;

            if (timer >= lifeTime)
            {
                ReturnToPool();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            OnHit?.Invoke(other, this);

            ReturnToPool();
        }

        private void ReturnToPool()
        {
            ProjectilePool.Instance.ReturnToPool(this.gameObject);
        }
    }
}
