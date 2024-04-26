using System.Collections.Generic;

namespace FSM.state.impls {
    public abstract class State<TE> : IState<TE> {
        protected FSM<TE> Fsm;

        private readonly Dictionary<TE, IState<TE>> _transitions = new();

        public virtual void Enter() {
        }

        public virtual void Execute() {
        }

        public virtual void Sleep() {
        }

        public void Add(TE transition, IState<TE> state) {
            this._transitions[transition] = state;
        }

        public void Remove(TE transition, IState<TE> state) {
            if (this._transitions.ContainsKey(transition)) this._transitions.Remove(transition);
        }

        public void Remove(IState<TE> state) {
            foreach (KeyValuePair<TE, IState<TE>> transition in this._transitions) {
                if (transition.Value == state) this._transitions.Remove(transition.Key);
            }
        }

        public IState<TE> Get(TE transition) {
            return this._transitions.TryGetValue(transition, out IState<TE> t) ? t : null;
        }

        public void SetFSM(FSM<TE> fsm) {
            this.Fsm = fsm;
        }
    }
}