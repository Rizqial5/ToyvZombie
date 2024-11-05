using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TvZ.Character
{
    public class CharStat : MonoBehaviour
    {
        [SerializeField] float charHealth;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        


        public void DamageHealth(float damage)
        {
            if (charHealth <= 0)
            {
                Destroy(gameObject);
            }

            charHealth -= damage;

            print(gameObject.name + " Terkena Damage");

            StartCoroutine(TakeDamageEffect());
        }

        private IEnumerator TakeDamageEffect()
        {
            spriteRenderer.color = Color.red;

            yield return new WaitForSeconds(0.2f);

            spriteRenderer.color = Color.white;
        }
    }
}
