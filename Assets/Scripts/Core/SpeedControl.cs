using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TvZ.Core
{
    public class SpeedControl : MonoBehaviour
    {
        public static SpeedControl Instance;

        public float speedModifierGame { get; private set; } = 1f;
        public float speedAnimation { get; private set; } = 1f;
        private Animator[] animators;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetSpeedGameTime(float setSpeed)
        {
            if(setSpeed == 2)
            {
                speedAnimation = 3f;
                speedModifierGame = 5f;
                Time.timeScale = 3f;
            }
        }

    }
}
