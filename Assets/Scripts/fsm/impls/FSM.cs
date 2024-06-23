using System.Diagnostics.CodeAnalysis;

namespace fsm.impls {
    public class Fsm<TState> {
        [AllowNull] private IState<TState> _state;

        public Fsm() {
        }

        public Fsm(IState<TState> state) {
            _state = state;
        }


        public void Update() {
            _state?.Execute();
        }

        public void Transition(TState input) {
            IState<TState> newState = _state.Get(input);
            if (newState == null) return;
            _state.Sleep();
            SetInit(newState);
        }

        public void SetInit(IState<TState> init) {
            _state = init;
            _state.SetFsm = this;
            _state.Enter();
        }
    }
}