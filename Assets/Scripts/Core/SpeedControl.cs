using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TvZ.Core
{
    public class SpeedControl : MonoBehaviour
    {

        [SerializeField] float speedModifier;
        private Animator[] animators;

      

        public void SetSpeedGameTime()
        {
            Time.timeScale = speedModifier;

            if(animators != null)
            {
                foreach (Animator item in animators)
                {
                    item.speed = speedModifier;
                }
            }
        }

    }
}
