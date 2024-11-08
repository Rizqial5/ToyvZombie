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
            timeSystem.ChangeStatusButton("Skip To Day", true);

            timeSystem.timerCountDown.StartCountdown();
            timeSystem.timerCountDown.onTimerEnd.AddListener(() => { timeStateMachine.ChangeState(timeSystem.dayTimeState); });

            timeSystem.enemyManager.GenerateEnemyAuto();
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
            timeSystem.goldIncome.GainGoldIncome();

            timeSystem.AddCountDay();

            timeSystem.enemyManager.StopGenerateEnemy();

            
        }
    }
}
