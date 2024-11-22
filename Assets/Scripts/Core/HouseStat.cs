using System.Collections;
using System.Collections.Generic;
using TMPro;
using TvZ.TimeMechanic;
using UnityEngine;
using UnityEngine.Events;

namespace TvZ.Core
{
    public class HouseStat : MonoBehaviour
    {
        [Header("UI Element")]
        [SerializeField] GameObject gameOverUI;
        
        [SerializeField] TextMeshProUGUI totalDayValueText;

        private TimerCountDown timerCountDown;
        public UnityEvent onGameOver;
        
       public void GameOver()
       {
            timerCountDown = FindAnyObjectByType<TimerCountDown>();

            GameManager.Instance.SetPause(true);

            gameOverUI.SetActive(true);

            onGameOver.Invoke();

            totalDayValueText.text = timerCountDown.GetComponent<TimeSystem>().dayElapsed.ToString();

            timerCountDown.StopTimer();
            



       }
    }
}
