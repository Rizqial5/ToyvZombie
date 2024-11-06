using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TvZ.Core
{
    public class SnapPoint : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private BoxCollider2D boxCollider;


        public bool isOccupied { get; private set; }


        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        //private void Start()
        //{
        //    ActivateView(false);
        //}

        public void ActivateView(bool enable)
        {
            spriteRenderer.enabled = enable;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                isOccupied = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                isOccupied = false;

                
            }
        }
    }
}
