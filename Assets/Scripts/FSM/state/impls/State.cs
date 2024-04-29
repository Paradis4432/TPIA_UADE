using System.Collections.Generic;
using UnityEngine;

namespace FSM.state.impls {
    public abstract class State<TE> : IState<TE> {
        protected FSM<TE> Fsm;
        protected Transform _target;

        private readonly Dictionary<TE, IState<TE>> _transitions = new();

        public virtual void Enter() {
        }

        public virtual void Execute() {
        }

        public virtual void Sleep() {
        }

        public void Add(TE transition, IState<TE> state) {
            _transitions[transition] = state;
        }

        public void Remove(TE transition, IState<TE> state) {
            if (_transitions.ContainsKey(transition)) _transitions.Remove(transition);
        }

        public void Remove(IState<TE> state) {
            foreach (KeyValuePair<TE, IState<TE>> transition in _transitions) {
                if (transition.Value == state) _transitions.Remove(transition.Key);
            }
        }

        public IState<TE> Get(TE transition) {
            return _transitions.TryGetValue(transition, out IState<TE> t) ? t : null;
        }

        public void SetFSM(FSM<TE> fsm) {
            Fsm = fsm;
        }

        public void SetTarget(Transform target) {
            this._target = target;
        }
    }
}