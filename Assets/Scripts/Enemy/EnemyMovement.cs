using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TvZ.Character;


namespace TvZ.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {

        [SerializeField] float speed;
        private Transform destinationPos;
        private float stopDistance = 0.1f;
        private DetectionArea detectionArea;

        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            detectionArea = GetComponentInChildren<DetectionArea>();
        }

        private void FixedUpdate()
        {
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
                Destroy(gameObject);
            }
            
            
           
        }

        public void SetEnemy(Transform destination)
        {
            destinationPos = destination;
        }
    }
}
