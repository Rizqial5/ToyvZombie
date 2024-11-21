using System.Collections.Generic;
using TvZ.Character;
using TvZ.Core;
using TvZ.TimeMechanic;
using UnityEngine;
using UnityEngine.Pool;

namespace TvZ.Enemy
{
    public class EnemyManager : MonoBehaviour
    {

        [SerializeField] StageProgressionSO stageProgressionSO;

        [SerializeField] Transform[] startLoc;
        [SerializeField] Transform[] finishLoc;
        

        private HouseStat houseStat;
        private List<StatSO> enemyPrefab = new List<StatSO>();

        private Dictionary<StatSO, ObjectSpawner> enemyDictPool = new Dictionary<StatSO, ObjectSpawner>();

        private ObjectSpawner enemyPool;
        
        private TimeSystem timeSystem;

        //private Dictionary<StatSO, ObjectPool<GameObject>> enemyPoolDict = new Dictionary<StatSO, ObjectPool<GameObject>>();
        

        private void Awake()
        {
            houseStat = FindAnyObjectByType<HouseStat>();
            timeSystem = FindAnyObjectByType<TimeSystem>();
            enemyPool = GetComponent<ObjectSpawner>();
            
            
        }


        private void GenerateEnemy()
        {

            if(timeSystem.dayElapsed > stageProgressionSO.GetEnemyLevelMax())
            {
                enemyPrefab = stageProgressionSO.GetEnemyLevelList(stageProgressionSO.GetEnemyLevelMax());
            }
            else
            {
                enemyPrefab = stageProgressionSO.GetEnemyLevelList(timeSystem.dayElapsed);
            }

            int randomInt = Random.Range(0, startLoc.Length);
            int randomIntEnemies = Random.Range(0, enemyPrefab.Count);

            StatSO enemySelected = enemyPrefab[randomIntEnemies];






            enemyPool.SetObjectPrefab(enemySelected.GetCharPrefab(), this.transform, this.transform);



            //GameObject spawnedEnemy = Instantiate(enemySelected.GetCharPrefab(), startLoc[randomInt].position, Quaternion.identity);

            GameObject spawnedEnemy = enemyPool._pool.Get();

            spawnedEnemy.transform.position = startLoc[randomInt].position;

            spawnedEnemy.GetComponent<EnemyMovement>().SetEnemy(finishLoc[randomInt], houseStat.GameOver);
        }

        public void GenerateEnemyAuto()
        {
            GenerateEnemy();
        }

        

        public void StopGenerateEnemy()
        {
            CancelInvoke("GenerateEnemy");

           //
        }
    }
}
