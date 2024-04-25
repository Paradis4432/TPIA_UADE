// finite state machine

using FSM.state;

namespace FSM {
    public class FSM<TE> {
        private IState<TE> _state;

        public FSM() {
        }

        public FSM(IState<TE> state) {
            this.SetState(state);
        }

        public void SetState(IState<TE> state) {
            this._state = state;
        }

        public void Update() {
            this._state?.Execute();
        }
        
        public void Transition(TE transition) {
            IState<TE> state = this._state.Get(transition);

            if (state == null) return;
            
            this._state.Sleep();
            this.SetState(state);
            this._state.SetFSM(this);
            this._state.Enter();
        }
    }
}