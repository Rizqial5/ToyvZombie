using System.Collections;
using System.Collections.Generic;
using TMPro;
using TvZ.Enemy;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TvZ.TimeMechanic
{
    public class TimeSystem : MonoBehaviour
    {
        #region State Builder

        public TimeStateMachine timeStateMachine { get; set; }
        public DayTime dayTimeState { get; set; }
        public NightTime nightTimeState { get; set; }

        #endregion

        [SerializeField] TextMeshProUGUI timeStatusText;
        [SerializeField] Button changeButton;

        [Header("Menu UI")]
        [SerializeField] GameObject daytimeMenuUI;

        [Header("Debug Option")]
        [SerializeField] Vector2 positionDebug = new Vector2(100, 0);

        public EnemyManager enemyManager {  get; private set; }

        public TimerCountDown timerCountDown { get; private set; }

        private int dayElapsed;

        public UnityEvent onDayChanged;

        private void Awake()
        {
            timeStateMachine = new TimeStateMachine();
            dayTimeState = new DayTime(this, timeStateMachine);
            nightTimeState = new NightTime(this, timeStateMachine);

            timerCountDown = GetComponent<TimerCountDown>();
            enemyManager = GetComponent<EnemyManager>();
        }

        private void Start()
        {
            timeStateMachine.Initialize(dayTimeState);
        }

        private void Update()
        {
            timeStateMachine.currentTimeState.FrameUpdate();
        }

        private void FixedUpdate()
        {
            timeStateMachine.currentTimeState.PhysicsUpdate();
        }


        public void SkipDay()
        {
            if (timeStateMachine.currentTimeState == dayTimeState)
            {
                timeStateMachine.ChangeState(nightTimeState);
            }
            else if (timeStateMachine.currentTimeState == nightTimeState)
            {
                timeStateMachine.ChangeState(dayTimeState);
            }
            
        }

        public void ChangeStatusButton(string text, bool isEnable)
        {
            changeButton.gameObject.SetActive(isEnable);
            timeStatusText.text = text;


            daytimeMenuUI.SetActive(isEnable);
        }

        public void AddCountDay()
        {
            dayElapsed += 1;
            onDayChanged.Invoke();
        }

        

        private void OnGUI()
        {

            GUIStyle textStyle = new GUIStyle(GUI.skin.label);
            textStyle.fontSize = 40;
            textStyle.normal.textColor = Color.white;

            
            // Menampilkan teks di posisi yang ditentukan
            GUI.Label(new Rect(positionDebug.x, positionDebug.y, 800, 50), timeStateMachine.currentTimeState.ToString(), textStyle);
        }
    }
}
