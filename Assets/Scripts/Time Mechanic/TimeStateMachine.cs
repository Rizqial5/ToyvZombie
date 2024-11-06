using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TvZ.Character;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

namespace TvZ.TimeMechanic
{
    public class TimeStateMachine 
    {
        public TimeState currentTimeState { get; set; }
        public TimeState oldTimeState { get; set; }

        public void Initialize(TimeState starttingState)
        {
            currentTimeState = starttingState;

            Debug.Log(currentTimeState);

            currentTimeState.EnterState();
        }

        public void ChangeState(TimeState newState)
        {
            oldTimeState = currentTimeState;

            currentTimeState.ExitState();

            currentTimeState = newState;

            currentTimeState.EnterState();
        }

        
    }
}
