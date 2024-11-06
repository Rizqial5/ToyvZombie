using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TvZ.TimeMechanic
{
    public class TimeState 
    {
        protected TimeSystem timeSystem;
        protected TimeStateMachine timeStateMachine;

        public TimeState(TimeSystem timeSystem, TimeStateMachine timeStateMachine)
        {
            this.timeSystem = timeSystem;
            this.timeStateMachine = timeStateMachine;
        }

        public virtual void EnterState() { }
        public virtual void ExitState() { }
        public virtual void FrameUpdate() { }
        public virtual void PhysicsUpdate() { }
    }
}
