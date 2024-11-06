using System.Collections;
using System.Collections.Generic;
using TvZ.Character;
using UnityEngine;
using UnityEngine.Events;

namespace TvZ.Character
{
    public class BulletPhysics : MonoBehaviour
    {
        public float speed = 10f; // Kecepatan bullet

        private float bulletDamage;
        private Vector2 targetPosition; // Posisi target yang diprediksi

        private Transform target;
        private Rigidbody2D rb;
        private Collider2D thisCollider;

        public UnityEvent onHitChar;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            thisCollider = GetComponent<Collider2D>();
        }

        private void Start()
        {
            Destroy(gameObject, 4f);

            IgnoreCollider();

        }

        private void IgnoreCollider()
        {
            GameObject[] objectsToIgnore = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject obj in objectsToIgnore)
            {
                Collider2D otherCollider = obj.GetComponent<Collider2D>();
                if (otherCollider != null)
                {
                    Physics2D.IgnoreCollision(thisCollider, otherCollider);
                }
            }
        }

        public void SetTarget(Transform target, float setDamage)
        {
            this.target = target;
            bulletDamage = setDamage;

        }



        void FixedUpdate()
        {
            if (target == null) return;
            targetPosition = (target.position - transform.position).normalized;

            rb.velocity = targetPosition * speed;


        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
                collision.gameObject.GetComponent<CharStat>().DamageHealth(bulletDamage);
                print("Kena");
                onHitChar.Invoke();
            }

            if(collision.gameObject.CompareTag("Player"))
            {
                
            }



        }
    }
}
