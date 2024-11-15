using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TvZ.Enemy;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TvZ.TimeMechanic
{
    public class TimerCountDown : MonoBehaviour
    {
        
        [SerializeField] TextMeshProUGUI timerText;             
        [SerializeField] int countdownTime = 300;

        [SerializeField] Button pauseButton;
        [SerializeField] Button resumeButton;

        private DateTime endTime;          
        private bool isCountingDown = false;
        private bool isPaused = false;
        private bool isStopped;
        private TimeSpan remainingTime;
        private RepeatedAction repeatedAction;

        public UnityEvent onTimerEnd; 

       

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
            endTime = DateTime.Now.AddSeconds(countdownTime); 
            isCountingDown = true;
            isPaused = false;
        }

        
        void UpdateTimer()
        {
            

            TimeSpan remainingTime = endTime - (DateTime.Now); 


            
            if (remainingTime.TotalSeconds <= 0)
            {
                isCountingDown = false;
                timerText.text = "00:00";
                OnTimerEnd(); 
            }
            else
            {
                timerText.text = string.Format("{0:D2}:{1:D2}", remainingTime.Minutes, remainingTime.Seconds);
            }
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
