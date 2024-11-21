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

        [SerializeField] Transform attachedTransform;

        [SerializeField] Transform parentObject;

         

        private void Start()
        {
            _pool = new ObjectPool<GameObject>(CreateObjectPool, OnTakeObjectFromPool, OnReturnObject, OnDestroyObject, true, 1000,2000);
            
        }

        public void SetObjectPrefab(GameObject prefabObject, Transform attachedTransform, Transform parentObject)
        {
            this.prefabObject = prefabObject;
            this.parentObject = parentObject;
            this.attachedTransform = attachedTransform;

            
        }

        private GameObject CreateObjectPool()
        {
            GameObject spawnedObject;

            if(parentObject == null)
            {
                spawnedObject = Instantiate(prefabObject, attachedTransform.position, attachedTransform.rotation);
            }
            else
            {
                spawnedObject = Instantiate(prefabObject, attachedTransform.position, attachedTransform.rotation, parentObject);
            }
            

            spawnedObject.GetComponent<IPoolAble>().SetPool(_pool);

            return spawnedObject;
        }

        private void OnTakeObjectFromPool(GameObject objectPrefab)
        {
            objectPrefab.transform.position = attachedTransform.position;

            objectPrefab.gameObject.SetActive(true);
        }

        private void OnReturnObject(GameObject objectPrefab)
        {
            objectPrefab.gameObject.SetActive(false);
        }

        private void OnDestroyObject(GameObject objectPrefab)
        {
            Destroy(objectPrefab.gameObject);
        }

    }
}
