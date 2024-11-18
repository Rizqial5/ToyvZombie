using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TvZ.Character;
using UnityEngine.Events;


namespace TvZ.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {

        [SerializeField] float speed;
        private Transform destinationPos;
        private float stopDistance = 0.1f;
        private DetectionArea detectionArea;

        private Rigidbody2D rb;

        public UnityEvent onReachedTarget;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            detectionArea = GetComponentInChildren<DetectionArea>();
        }

        private void Start()
        {
            GetComponent<CharStat>().onCharDie.AddListener(() => { speed = 0; });
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.isPaused) return;

            MoveToTarget();
        }

        private void MoveToTarget()
        {
            float distance = Vector3.Distance(transform.position, destinationPos.position);

            if (detectionArea.isDetected) return;

            if(distance > stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, destinationPos.position, speed * Time.fixedDeltaTime);

            }else if (distance <= stopDistance)
            {
                onReachedTarget.Invoke();
                Destroy(gameObject);
            }
            
            
           
        }

        public void SetEnemy(Transform destination, UnityAction reachedEvent)
        {
            destinationPos = destination;

            onReachedTarget.AddListener(reachedEvent);
        }
    }
}
