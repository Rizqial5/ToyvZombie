using TvZ.Core;
using UnityEngine;

namespace TvZ.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] GameObject enemyPrefab;

        [SerializeField] Transform[] startLoc;
        [SerializeField] Transform[] finishLoc;

        private HouseStat houseStat;

        private void Awake()
        {
            houseStat = FindAnyObjectByType<HouseStat>();
        }
        

        private void GenerateEnemy()
        {
            int randomInt = Random.Range(0, startLoc.Length);

            

            GameObject spawnedEnemy = Instantiate(enemyPrefab, startLoc[randomInt].position,Quaternion.identity);

            spawnedEnemy.GetComponent<EnemyMovement>().SetEnemy(finishLoc[randomInt], houseStat.GameOver);
        }

        public void GenerateEnemyAuto()
        {
            InvokeRepeating("GenerateEnemy", 2f, 2f);
        }

        public void StopGenerateEnemy()
        {
            CancelInvoke("GenerateEnemy");
        }
    }
}
