using System.Collections;
using System.Collections.Generic;
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
        

        private bool isFired;
        private void Awake()
        {
            detectionArea = GetComponentInChildren<DetectionArea>();
            animator = GetComponent<Animator>();
            
            
        }

        private void Start()
        {
            bulletDamage = charStatSO.GetCharStat(StatEnum.Damage);
        }

        private void Update()
        {
            //if (!basePlayer.isPlayerOnField) return; cek apabila karakter sudah ada dilapangan

            timeUntilFired += Time.deltaTime;


            if (detectionArea.isDetected)
            {
                if (timeUntilFired >= 1f / bulletPerSecond)
                {

                    ShotTarget(detectionArea.charDetected.transform);


                    //animator.SetTrigger("tAttack");

                    timeUntilFired = 0f;

                }
                
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

        public void ShotBullet()
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);


            bullet.GetComponent<BulletPhysics>().SetTarget(targetEnemy, bulletDamage);
        }
    }

}