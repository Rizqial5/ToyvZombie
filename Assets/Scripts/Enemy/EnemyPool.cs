using System.Collections;
using System.Collections.Generic;
using TvZ.Character;
using TvZ.Interfaces;
using UnityEngine;
using UnityEngine.Pool;

namespace TvZ.Enemy
{
    public class EnemyPool : MonoBehaviour
    {
        public Dictionary<StatSO, ObjectPool<GameObject>> _poolDictionary {  get; private set; } = new Dictionary<StatSO, ObjectPool<GameObject>>();

        [SerializeField] List<StatSO> enemyLists = new List<StatSO>();

        private StatSO selectedEnemy;

        // Start is called before the first frame update
        void Start()
        {
            CreateDictionaryPool();
        }

       

        public void SetSelectedEnemy(StatSO enemyStat)
        {
            selectedEnemy = enemyStat;
        }

        private void CreateDictionaryPool()
        {
            foreach (StatSO item in enemyLists)
            {
                ObjectPool<GameObject> enemyPool = new ObjectPool<GameObject>(CreateEnemyPool, OnTakeObjectFromPool, OnReturnObject, OnDestroyObject, true, 10, 50);

                _poolDictionary[item] = enemyPool;
            }
        }

        private GameObject CreateEnemyPool()
        {
            GameObject spawnedEnemy;



            foreach (StatSO item in enemyLists)
            {
                if(item == selectedEnemy)
                {
                     spawnedEnemy = Instantiate(item.GetCharPrefab(), transform.position, Quaternion.identity);

                     spawnedEnemy.GetComponent<IPoolAble>().SetPool(_poolDictionary[selectedEnemy]);

                    return spawnedEnemy;
                }
            }

            return null;
        }

        private void OnTakeObjectFromPool(GameObject objectPrefab)
        {

            objectPrefab.transform.position = transform.position;

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
