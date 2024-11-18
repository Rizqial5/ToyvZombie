using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TvZ.Character
{
    public class CharStat : MonoBehaviour
    {

        [SerializeField] StatSO charStatSO;

        private float charHealth;

        

        private SpriteRenderer spriteRenderer;

        public UnityEvent onCharDie;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            
        }

        private void Start()
        {
            charHealth = charStatSO.GetCharStat(StatEnum.Health);

            
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

                Destroy(gameObject, 2f);
                float randomYPos = Random.Range(-10, 10);
                Vector3 rotationChar = new Vector3(0, 0, 360);
                transform.DOMoveX(15f, 1);
                transform.DOMoveY(randomYPos, 1);
                transform.DORotate( rotationChar , 0.2f, RotateMode.FastBeyond360).SetLoops(-1);

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
    }
}
