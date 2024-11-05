using UnityEngine;

namespace TvZ.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] GameObject enemyPrefab;

        [SerializeField] Transform[] startLoc;
        [SerializeField] Transform[] finishLoc;

        private void Start()
        {
            //InvokeRepeating("GenerateEnemy", 0.5f, 0.5f);
        }

        private void Update()
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Began )
                {
                    GenerateEnemy();
                }
            }
        }

        private void GenerateEnemy()
        {
            //int randomInt = Random.Range(0, startLoc.Length);

            int randomInt = 2;

            GameObject spawnedEnemy = Instantiate(enemyPrefab, startLoc[randomInt].position,Quaternion.identity);

            spawnedEnemy.GetComponent<EnemyMovement>().SetEnemy(finishLoc[randomInt]);
        }
    }
}
