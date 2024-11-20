using System.Collections;
using System.Collections.Generic;
using TvZ.Core;
using UnityEngine;
using UnityEngine.Events;

namespace TvZ.Character
{
    public class RangedChar : MonoBehaviour
    {
        [SerializeField] StatSO charStatSO;
        [SerializeField] GameObject bulletPrefab;
        

        [SerializeField] float bulletPerSecond = 1f;

        private float bulletDamage;



        private float timeUntilFired;

        private Transform targetEnemy;
        private DetectionArea detectionArea;
        private Animator animator;
        private AudioSource audioSource;
        

        private bool isFired;
        private void Awake()
        {
            detectionArea = GetComponentInChildren<DetectionArea>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            
            
        }

        private void Start()
        {
            bulletDamage = charStatSO.GetCharStat(StatEnum.Damage);
            audioSource.clip = charStatSO.damageClip;
        }

        private void Update()
        {
            if (GameManager.Instance.isPaused) return;

            timeUntilFired += Time.deltaTime;


            if (detectionArea.isDetected)
            {
                

                ShotTarget(detectionArea.charDetected.transform);


                   
                
            }
            else if (!detectionArea.isDetected)
            {
                animator.SetBool("isAttack", false);
            }

        }

        public void ShotTarget(Transform targetEnemy)
        {
            if (targetEnemy == null) return;

            this.targetEnemy = targetEnemy;
            
            
            animator.SetBool("isAttack", true);

            
            

        }

        public void ShotSound()
        {
            audioSource.Play();
            //
        }

        public void ShotBullet()
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);


            bullet.GetComponent<BulletPhysics>().SetTarget(targetEnemy, bulletDamage);
        }
    }

}