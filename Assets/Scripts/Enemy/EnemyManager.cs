using System.Collections.Generic;
using TvZ.Character;
using TvZ.Core;
using TvZ.TimeMechanic;
using UnityEngine;

namespace TvZ.Enemy
{
    public class EnemyManager : MonoBehaviour
    {

        [SerializeField] StageProgressionSO stageProgressionSO;

        [SerializeField] Transform[] startLoc;
        [SerializeField] Transform[] finishLoc;

        private HouseStat houseStat;
        private List<StatSO> enemyPrefab = new List<StatSO>();
        private TimeSystem timeSystem;

        private void Awake()
        {
            houseStat = FindAnyObjectByType<HouseStat>();
            timeSystem = GetComponent<TimeSystem>();
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

            

            GameObject spawnedEnemy = Instantiate(enemyPrefab[randomIntEnemies].GetCharPrefab(), startLoc[randomInt].position,Quaternion.identity);

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
