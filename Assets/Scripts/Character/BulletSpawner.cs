using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace TvZ.Character
{
    public class BulletSpawner: MonoBehaviour
    {
        public ObjectPool<GameObject> _pool;

        [SerializeField] GameObject prefabObject;

        private void Start()
        {
            _pool = new ObjectPool<GameObject>(CreateBulletPool, OnTakeBulletFromPool, OnReturnBullet, OnDestroyBullet, true, 1000,2000);
        }

        private GameObject CreateBulletPool()
        {
            GameObject spawnedBullet = Instantiate(prefabObject,transform.position, transform.rotation,transform);

            spawnedBullet.GetComponent<BulletPhysics>().SetPool(_pool);

            return spawnedBullet;
        }

        private void OnTakeBulletFromPool(GameObject bullet)
        {
            bullet.transform.position = transform.position;

            bullet.gameObject.SetActive(true);
        }

        private void OnReturnBullet(GameObject bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        private void OnDestroyBullet(GameObject bullet)
        {
            Destroy(bullet.gameObject);
        }

    }
}
