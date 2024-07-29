using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.ProjectileSystem
{
    public class ProjectilePool : MonoBehaviour
    {
        public static ProjectilePool Instance { get; private set; }
        public GameObject projectilePrefab;
        public int poolSize = 20;

        private Queue<GameObject> pool;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                pool = new Queue<GameObject>();
                for (int i = 0; i < poolSize; i++)
                {
                    GameObject obj = Instantiate(projectilePrefab, transform);
                    obj.SetActive(false);
                    pool.Enqueue(obj);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public GameObject GetFromPool()
        {
            if (pool.Count > 0)
            {
                GameObject obj = pool.Dequeue();
                obj.SetActive(true);
                return obj;
            }
            else
            {
                GameObject obj = Instantiate(projectilePrefab);
                return obj;
            }
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

}
