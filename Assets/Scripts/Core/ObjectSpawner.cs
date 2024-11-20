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
            _pool = new ObjectPool<GameObject>(CreateObjectPool, OnTakeObjectFromPool, OnReturnObject, OnDestroyObject, true, 1000,2000);
        }

        public void SetObjectPrefab(GameObject prefabObject)
        {
            this.prefabObject = prefabObject;
        }

        private GameObject CreateObjectPool()
        {
            GameObject spawnedObject = Instantiate(prefabObject,transform.position, transform.rotation,transform);

            spawnedObject.GetComponent<IPoolAble>().SetPool(_pool);

            return spawnedObject;
        }

        private void OnTakeObjectFromPool(GameObject objectPrefab)
        {
            objectPrefab.transform.position = transform.position;

            objectPrefab.gameObject.SetActive(true);
        }

        private void OnReturnObject(GameObject pbjectPrefab)
        {
            pbjectPrefab.gameObject.SetActive(false);
        }

        private void OnDestroyObject(GameObject objectPrefab)
        {
            Destroy(objectPrefab.gameObject);
        }

    }
}
