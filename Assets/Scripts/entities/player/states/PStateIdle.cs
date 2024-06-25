using fsm.states.impls;
using UnityEngine;

namespace entities.player.states {
    public class PStateIdle<T> : State<T> {
        private readonly T _input;

        public PStateIdle(T input) {
            _input = input;
        }

        public override void Execute() {
            base.Execute();
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            if (x == 0 && y == 0) return;
            Fsm.Transition(_input);
        }
    }
}