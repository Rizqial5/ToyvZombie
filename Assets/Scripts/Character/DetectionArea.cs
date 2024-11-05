using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TvZ.Character
{
    public class DetectionArea : MonoBehaviour
    {

        public bool isDetected {  get; private set; }

        public GameObject charDetected {  get; private set; }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if(collision.tag == "Player" || collision.tag == "Enemy")
            {
                if (collision.tag == gameObject.tag) return;

                charDetected = collision.gameObject;
                isDetected = true;
            }
        }

        
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player" || collision.tag == "Enemy")
            {
                if (collision.tag == gameObject.tag) return;

                charDetected = null;
                isDetected = false;
            }
        }
    }
}
