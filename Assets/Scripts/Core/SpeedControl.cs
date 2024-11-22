using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TvZ.Core
{
    public class SpeedControl : MonoBehaviour
    {
        public static SpeedControl Instance;

        public float speedModifierGame { get; private set; } = 1f;
        public float speedAnimation { get; private set; } = 1f;
        public float speedSpawn {  get; private set; } = 1f;
        private Animator[] animators;

        [SerializeField] Button speedButton;
        [SerializeField] Button normalButton;

        private void Start()
        {
            speedAnimation = 1f;
            speedModifierGame = 1f;
            Time.timeScale = 1f;
        }
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
                speedAnimation = 1.5f;
                speedSpawn = 2f;
                speedModifierGame = 5f;
                Time.timeScale = 2f;

            } else if(setSpeed == 1)
            {
                speedAnimation = 1f;
                speedSpawn = 1f;
                speedModifierGame = 1f;
                Time.timeScale = 1f;
            }
        }

        public void SetButtonActive(bool active)
        {
            speedButton.gameObject.SetActive(active);
            normalButton.gameObject.SetActive(!active);
        }

    }
}
