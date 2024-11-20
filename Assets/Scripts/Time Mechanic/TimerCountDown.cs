using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TvZ.Core;
using TvZ.Enemy;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TvZ.TimeMechanic
{
    public class TimerCountDown : MonoBehaviour
    {
        [SerializeField] int countdownTime = 300;
        [SerializeField] TextMeshProUGUI timerText;             
        [SerializeField] Button resumeButton;
        [SerializeField] Button pauseButton;

        private DateTime endTime;  
        private DateTime startTime;
        private bool isCountingDown = false;
        private bool isPaused = false;
        private bool isStopped;
        private bool isFunctionActivated;  

        private TimeSpan remainingTime;
        private RepeatedAction repeatedAction;
        private DateTime nowTime;

        public UnityEvent onTimerEnd;
        public UnityEvent on5SecondsLeft;





        void Update()
        {
            if(isStopped) return;

            if (isCountingDown && !isPaused)
            {
                UpdateTimer();
            }
        }

        private void Start()
        {
            repeatedAction = GetComponent<RepeatedAction>();

           
        }

        public void StartCountdown()
        {
            remainingTime = TimeSpan.FromSeconds(countdownTime);
            startTime = DateTime.Now;
            isCountingDown = true;
            isPaused = false;
            isFunctionActivated = false;
        }


        void UpdateTimer()
        {

            nowTime = DateTime.Now;

            double elapsedSeconds = (nowTime - startTime).TotalSeconds * SpeedControl.Instance.speedModifierGame;
            remainingTime -= TimeSpan.FromSeconds(elapsedSeconds);

            startTime = nowTime;


            if (remainingTime.TotalSeconds <= 0)
            {
                isCountingDown = false;
                timerText.text = "00:00";
                OnTimerEnd();
            }
            else if (remainingTime.TotalSeconds > 0 && remainingTime.TotalSeconds <= 5f)
            {
                if(!isFunctionActivated)
                {
                    on5SecondsLeft.Invoke();
                    isFunctionActivated = true;
                }
            }
            

            timerText.text = string.Format("{0:D2}:{1:D2}", remainingTime.Minutes, remainingTime.Seconds);
        }

        
        public void PauseTimer()
        {
            if (!isPaused && isCountingDown)
            {
                isPaused = true;
                remainingTime = endTime - DateTime.Now; 
            }

            ChangeButtonActive();
        }

        
        public void ResumeTimer()
        {
            if (isPaused && isCountingDown)
            {
                isPaused = false;
                endTime = DateTime.Now.Add(remainingTime); 
            }
            //
            ChangeButtonActive();
        }

        public void StopTimer()
        {
            isStopped = true;
        }

        public void ChangeButtonActive()
        {
            if(isPaused)
            {
                pauseButton.gameObject.SetActive(false);
                resumeButton.gameObject.SetActive(true);
            }
            else if(!isPaused)
            {
                pauseButton.gameObject.SetActive(true);
                resumeButton.gameObject.SetActive(false);
            }
        }

        

        // Fungsi yang dipanggil saat timer berakhir
        void OnTimerEnd()
        {
            Debug.Log("Timer has ended!");
            onTimerEnd.Invoke();
        }
    }
}
