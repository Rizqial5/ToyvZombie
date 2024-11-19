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

            //timeSystem.repeatedAction.StartRepeatingAction();
        }


        public override void FrameUpdate()
        {
            //timeSystem.repeatedAction.PauseRepeatingAction();
            //timeSystem.repeatedAction.ResumeRepeatingAction();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void ExitState()
        {
            timeSystem.goldIncome.GainGoldIncome();
            timeSystem.goldIncome.GainBluePrint();

            timeSystem.EndNightTime();

            timeSystem.AddCountDay();
            ///
            //timeSystem.repeatedAction.StopRepeatingAction();


        }
    }
}
