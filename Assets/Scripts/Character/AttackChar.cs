using System.Collections;
using System.Collections.Generic;
using TvZ.Core;
using UnityEngine;

namespace TvZ.Character
{
    public class AttackChar : MonoBehaviour
    {
        [SerializeField] StatSO charStatSO;
        private float damagePoint;

        private DetectionArea detectionArea;
        private Animator animator;
        private AudioSource audioSource;

        

        private void Awake()
        {
            detectionArea = GetComponentInChildren<DetectionArea>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();

            
        }

        private void Start()
        {
            damagePoint = charStatSO.GetCharStat(StatEnum.Damage);
            animator.speed = SpeedControl.Instance.speedAnimation;
        }
        private void Update()
        {
            

            if (detectionArea.isDetected)
            {
                animator.SetBool("isAttack", true);

            }
            else if (!detectionArea.isDetected)
            {
                animator.SetBool("isAttack", false);
            }

        }

        

        public void EnemyHit()
        {
            if (!detectionArea.charDetected) return;
            audioSource.clip = charStatSO.damageClip;

            audioSource.Play();

            detectionArea.charDetected.GetComponent<CharStat>().DamageHealth(damagePoint);
        }
    }
}
