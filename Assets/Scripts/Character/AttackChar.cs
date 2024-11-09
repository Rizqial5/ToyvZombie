using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TvZ.Character
{
    public class AttackChar : MonoBehaviour
    {
        [SerializeField] StatSO charStatSO;
        private float damagePoint;

        private DetectionArea detectionArea;
        private Animator animator;

        [SerializeField] float animatorSpeed = 1;

        private void Awake()
        {
            detectionArea = GetComponentInChildren<DetectionArea>();
            animator = GetComponent<Animator>();

            
        }

        private void Start()
        {
            damagePoint = charStatSO.GetCharStat(StatEnum.Damage);
            animator.speed = animatorSpeed;
        }
        private void Update()
        {
            

            if (GameManager.Instance.isPaused)
            {
                animator.speed = 0;
            }
            else if (!GameManager.Instance.isPaused)
            {
                animator.speed = animatorSpeed;

            }

            if (detectionArea.isDetected)
            {
                animator.SetBool("isAttack", true);

            }else if (!detectionArea.isDetected)
            {
                animator.SetBool("isAttack", false);
            }

        }

        public void EnemyHit()
        {
            if (!detectionArea.charDetected) return;

            detectionArea.charDetected.GetComponent<CharStat>().DamageHealth(damagePoint);
        }
    }
}
