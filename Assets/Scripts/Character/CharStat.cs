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
                Destroy(gameObject);

                onCharDie.Invoke();
            }

        }

        private IEnumerator TakeDamageEffect()
        {
            spriteRenderer.color = Color.red;

            yield return new WaitForSeconds(0.2f);

            spriteRenderer.color = Color.white;
        }
    }
}
