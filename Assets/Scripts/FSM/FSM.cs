// finite state machine

using FSM.state;
using UnityEngine;

namespace FSM {
    public class FSM<TE> {
        private IState<TE> _state;
        private Transform _target;

        public FSM() {
        }

        public FSM(IState<TE> state, Transform target = null) {
            SetState(state);
            _target = target;
        }

        public void SetState(IState<TE> state) {
            _state = state;
            state.SetTarget(_target);
            Debug.Log("setting state to " + state.GetType().Name);
        }

        public void Update() {
            _state?.Execute();
        }

        public void Transition(TE transition) {
            IState<TE> state = _state.Get(transition);

            if (state == null) return;

            _state.Sleep();
            SetState(state);
            _state.SetFSM(this);
            _state.Enter();
        }
    }
}