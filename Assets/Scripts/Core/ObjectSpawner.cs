using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using TvZ.Interfaces;

namespace TvZ.Core
{
    public class ObjectSpawner: MonoBehaviour
    {
        public ObjectPool<GameObject> _pool;

        [SerializeField] GameObject prefabObject;

        private void Start()
        {
            _pool = new ObjectPool<GameObject>(CreateBulletPool, OnTakeBulletFromPool, OnReturnBullet, OnDestroyBullet, true, 1000,2000);
        }

        public void SetObjectPrefab(GameObject prefabObject)
        {
            this.prefabObject = prefabObject;
        }

        private GameObject CreateBulletPool()
        {
            GameObject spawnedObject = Instantiate(prefabObject,transform.position, transform.rotation,transform);

            spawnedObject.GetComponent<IPoolAble>().SetPool(_pool);

            return spawnedObject;
        }

        private void OnTakeBulletFromPool(GameObject objectPrefab)
        {
            objectPrefab.transform.position = transform.position;

            objectPrefab.gameObject.SetActive(true);
        }

        private void OnReturnBullet(GameObject pbjectPrefab)
        {
            pbjectPrefab.gameObject.SetActive(false);
        }

        private void OnDestroyBullet(GameObject objectPrefab)
        {
            Destroy(objectPrefab.gameObject);
        }

    }
}
