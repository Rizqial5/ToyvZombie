using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TvZ.Enemy
{
    public class RepeatedAction : MonoBehaviour
    {
        private Coroutine repeatingCoroutine;
        public float interval = 1.0f; // Interval time for repeating action
        public UnityEvent repeatedEvent;
       
        private EnemyManager enemyManager;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
        }

        public void StartRepeatingAction()
        {
            if (repeatingCoroutine == null)
            {
                repeatingCoroutine = StartCoroutine(RepeatActionCoroutine());
            }
        }

        public void PauseRepeatingAction()
        {
            if(GameManager.Instance.isPaused)
            {
                if (repeatingCoroutine != null)
                {
                    StopCoroutine(repeatingCoroutine);
                    repeatingCoroutine = null;
                }
            }

            
        }

        public void ResumeRepeatingAction()
        {
            if(!GameManager.Instance.isPaused)
            {
                if (repeatingCoroutine == null)
                {
                    repeatingCoroutine = StartCoroutine(RepeatActionCoroutine());
                }
            }
            
        }

        public void StopRepeatingAction()
        {
            if (repeatingCoroutine != null)
            {
                StopCoroutine(repeatingCoroutine);
                repeatingCoroutine = null;
            }
        }

        private IEnumerator RepeatActionCoroutine()
        {
            while (true)
            {
                enemyManager.GenerateEnemyAuto();
                yield return new WaitForSeconds(interval);
            }
        }
    }
}
