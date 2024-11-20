using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TvZ.TimeMechanic
{
    public class NightTime : TimeState
    {
        public NightTime(TimeSystem timeSystem, TimeStateMachine timeStateMachine) : base(timeSystem, timeStateMachine)
        {
        }

        public override void EnterState()
        {
            

            timeSystem.ChangeDayStatus("Night Time");

            timeSystem.timerCountDown.StartCountdown();
            timeSystem.timerCountDown.onTimerEnd.AddListener(() => { timeStateMachine.ChangeState(timeSystem.dayTimeState); });

            timeSystem.repeatedAction.StartRepeatingAction();

            timeSystem.timerCountDown.on5SecondsLeft.AddListener(() => { timeSystem.repeatedAction.StopRepeatingAction(); });
        }


        public override void FrameUpdate()
        {
            
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void ExitState()
        {

            
            timeSystem.goldIncome.GainGoldIncome();
            timeSystem.goldIncome.GainBluePrint();

            timeSystem.speedControl.SetSpeedGameTime(1);

            timeSystem.EndNightTime();

            timeSystem.AddCountDay();

            timeSystem.timerCountDown.onTimerEnd.RemoveAllListeners();

            //timeSystem.repeatedAction.StopRepeatingAction();


        }
    }
}
