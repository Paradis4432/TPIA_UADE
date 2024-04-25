using System;
using FSM.state.impls;
using UnityEngine;

namespace entities.player.impls.states {
    public abstract class PlayerStates<TE> : State<TE> {
        protected TE Current;

        protected abstract void Execute();

        public class Idle<TE> : PlayerStates<TE> {
            public Idle(TE current) {
                this.Current = current;
            }

            protected override void Execute() {
                if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) return;
                this.Fsm.Transition(this.Current);
            }
        }

        public class Walk<TE> : PlayerStates<TE> {
            public Walk(TE current) {
                this.Current = current;
            }

            protected override void Execute() {
                
            }
        }
    }
}