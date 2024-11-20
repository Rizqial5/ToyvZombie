using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TvZ.Core;
using TvZ.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace TvZ.Character
{
    public class CharStat : MonoBehaviour, IPoolAble
    {

        [SerializeField] StatSO charStatSO;

        private float charHealth;
        private ObjectPool<GameObject> charPool;
        

        private SpriteRenderer spriteRenderer;
        private Animator animator;
       

        public UnityEvent onCharDie;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            
        }

        private void Start()
        {
            charHealth = charStatSO.GetCharStat(StatEnum.Health);

            
        }

        private void Update()
        {
            CharAnimSpeedControl();
        }

        public void CharAnimSpeedControl()
        {
            if (animator == null) return;

            if (GameManager.Instance.isPaused)
            {
                animator.speed = 0;
            }
            else if (!GameManager.Instance.isPaused)
            {
                animator.speed = SpeedControl.Instance.speedAnimation;

            }
        }
        public void DamageHealth(float damage)
        {
            charHealth -= damage;

            StartCoroutine(TakeDamageEffect());

            if (charHealth <= 0)
            {
                if (gameObject == null) return;
                DieAnimation();

                

                onCharDie.Invoke();
            }

        }

        private void ObjectDestroy()
        {
            Destroy(gameObject);
        }

        private IEnumerator TakeDamageEffect()
        {
            spriteRenderer.color = Color.red;

            yield return new WaitForSeconds(0.2f);

            spriteRenderer.color = Color.white;
        }

        private void DieAnimation()
        {
            if(gameObject.CompareTag("Enemy"))
            {

                GetComponent<Collider2D>().enabled = false;

                StartCoroutine(DestroyAfterTime(2f));

                float randomYPos = Random.Range(-10, 10);
                Vector3 rotationChar = new Vector3(0, 0, 360);
                transform.DOMoveX(15f, 1);
                transform.DOMoveY(randomYPos, 1);
                transform.DORotate( rotationChar , 0.5f, RotateMode.FastBeyond360).SetLoops(-1);

            }
            else if(gameObject.CompareTag("Player"))
            {
                GetComponent<Animator>().SetTrigger("TDie");
                //////
            }
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }

        public void SetPool(ObjectPool<GameObject> pool)
        {
            charPool = pool;
        }

        public void ReleaseObject()
        {
            charPool.Release(this.gameObject);
        }

        public IEnumerator DestroyAfterTime(float destroyTime)
        {
            yield return new WaitForSeconds(destroyTime);

            charPool.Release(this.gameObject);
        }
    }
}
