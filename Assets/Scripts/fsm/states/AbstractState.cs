using System.Collections.Generic;
using System.Linq;
using fsm.impls;
using UnityEngine;

namespace fsm.states {
    public abstract class AbstractState<T> : MonoBehaviour, IState<T> {
        protected Fsm<T> Fsm;
        protected Dictionary<T, IState<T>> Transitions = new();
        public abstract void Enter();
        public abstract void Execute();
        public abstract void Sleep();

        public void Add(T input, IState<T> state) {
            Transitions[input] = state;
        }

        public void Remove(T input) {
            if (Transitions.ContainsKey(input)) Transitions.Remove(input);
        }

        public void Remove(IState<T> state) {
            foreach (KeyValuePair<T, IState<T>> t in Transitions.Where(t => t.Value == state)) {
                Transitions.Remove(t.Key);
                break;
            }
        }

        public IState<T> Get(T input) {
            return Transitions.TryGetValue(input, out IState<T> transition) ? transition : null;
        }

        public Fsm<T> SetFsm {
            set => Fsm = value;
            get => Fsm;
        }
    }
}