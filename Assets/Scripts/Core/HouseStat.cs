using System.Collections;
using System.Collections.Generic;
using TMPro;
using TvZ.TimeMechanic;
using UnityEngine;

namespace TvZ.Core
{
    public class HouseStat : MonoBehaviour
    {
        [Header("UI Element")]
        [SerializeField] GameObject gameOverUI;
        
        [SerializeField] TextMeshProUGUI totalDayValueText;

        private TimerCountDown timerCountDown;

        
       public void GameOver()
       {
            //timerCountDown = FindAnyObjectByType<TimerCountDown>();

            //gameOverUI.SetActive(true);

            //totalDayValueText.text = timerCountDown.GetComponent<TimeSystem>().dayElapsed.ToString();

            //timerCountDown.StopTimer();
            ///
            

       }
    }
}
