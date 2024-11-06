using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TvZ.TimeMechanic
{
    public class DayTime : TimeState
    {
        public DayTime(TimeSystem timeSystem, TimeStateMachine timeStateMachine) : base(timeSystem, timeStateMachine)
        {
        }

        public override void EnterState()
        {
            timeSystem.ChangeStatusButton("Skip to Night", true);

            timeSystem.timerCountDown.StartCountdown();
            timeSystem.timerCountDown.onTimerEnd.AddListener(() => { timeStateMachine.ChangeState(timeSystem.nightTimeState); });
        }


        public override void FrameUpdate()
        {
            base.FrameUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }


        public override void ExitState()
        {
            base.ExitState();
        }
    }
}
