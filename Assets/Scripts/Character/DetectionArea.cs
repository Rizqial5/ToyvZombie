using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TvZ.Character
{
    public class DetectionArea : MonoBehaviour
    {
        public bool isDetected {  get; private set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                isDetected = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                isDetected = false;
            }
        }
    }
}
