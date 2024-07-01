using System.Collections.Generic;
using UnityEngine;

namespace fsm.states.impls {
    public abstract class State<TState> : AbstractState<TState> {
        private const bool DebugMode = true;

        private AbstractState<TState> _abstractStateImplementation;

        protected State() {
            Transitions = new Dictionary<TState, IState<TState>>();
        }

        protected State(Dictionary<TState, IState<TState>> transitions) {
            Transitions = transitions;
        }


        public override void Enter() {
        }

        public override void Execute() {
        }

        public override void Sleep() {
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected void Log(string s) {
            if (DebugMode)
                Debug.Log(s);
        }
    }
}