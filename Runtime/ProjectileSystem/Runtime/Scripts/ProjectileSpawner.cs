using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.ProjectileSystem
{

    public class ProjectileSpawner : MonoBehaviour
    {
        public Transform spawnPoint;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpawnProjectile(ProjectileHitAction);
            }
        }

        private void SpawnProjectile(Action<Collider, ProjectileBase> onHitAction)
        {
            GameObject projectile = ProjectilePool.Instance.GetFromPool();
            projectile.transform.position = spawnPoint.position;
            projectile.transform.rotation = spawnPoint.rotation;

            ProjectileBase projectileBase = projectile.GetComponent<ProjectileBase>();
            projectileBase.OnHit = onHitAction;
        }

        private void ProjectileHitAction(Collider other, ProjectileBase projectile)
        {
            Debug.Log("Projectile hit: " + other.name);

            //Health health = other.GetComponent<Health>();
            //if (health != null)
            //{
            //   health.TakeDamage(projectile.damage);
            //}
        }
    }

}
