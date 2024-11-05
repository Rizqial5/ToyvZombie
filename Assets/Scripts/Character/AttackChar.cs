using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TvZ.Character
{
    public class AttackChar : MonoBehaviour
    {
        [SerializeField] float damagePoint;

        private DetectionArea detectionArea;
        private Animator animator;

        private void Awake()
        {
            detectionArea = GetComponentInChildren<DetectionArea>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if(detectionArea.isDetected)
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
