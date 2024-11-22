using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using TvZ.Interfaces;

namespace TvZ.Character
{
    public class BulletPhysics : MonoBehaviour, IPoolAble
    {
        public float speed = 10f; // Kecepatan bullet

        private float bulletDamage;
        private Vector2 targetPosition; // Posisi target yang diprediksi

        private Transform target;
        private Rigidbody2D rb;
        private Collider2D thisCollider;

        private ObjectPool<GameObject> pool;
        private Coroutine deactivateBulletTimer;

        public UnityEvent onHitChar;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            thisCollider = GetComponent<Collider2D>();
        }

        private void OnEnable()
        {
            deactivateBulletTimer = StartCoroutine(DeactivateBulletTimer());
            IgnoreCollider();
        }

        private void Start()
        {
            
            
            IgnoreCollider();

        }
       

        void FixedUpdate()
        {
            if (GameManager.Instance.isPaused) return;



            rb.velocity = Vector2.right * speed;


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

        public void SetPool(ObjectPool<GameObject> pool)
        {
            this.pool = pool;
        }

        private IEnumerator DeactivateBulletTimer()
        {
            float bulletTimer = 4f;

            yield return new WaitForSeconds(bulletTimer);

            ReleaseObject();
            
        }

        

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                ReleaseObject();

                collision.gameObject.GetComponent<CharStat>().DamageHealth(bulletDamage);
                //print("Kena");
                onHitChar.Invoke();
            }

            if(collision.gameObject.CompareTag("Player"))
            {
                
            }



        }

        public void ReleaseObject()
        {
            pool.Release(this.gameObject);
        }
    }
}
