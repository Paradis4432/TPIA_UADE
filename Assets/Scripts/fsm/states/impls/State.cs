using System.Collections.Generic;

namespace fsm.states.impls {
    public class State<TState> : AbstractState<TState> {
        private AbstractState<TState> _abstractStateImplementation;

        public State() {
            Transitions = new Dictionary<TState, IState<TState>>();
        }

        public State(Dictionary<TState, IState<TState>> transitions) {
            Transitions = transitions;
        }


        public override void Enter() {
        }

        public override void Execute() {
        }

        public override void Sleep() {
        }
    }
}