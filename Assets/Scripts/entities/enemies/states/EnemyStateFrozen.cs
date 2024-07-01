using fsm.states;
using fsm.states.impls;
using UnityEngine;

namespace entities.enemies.states {
    public class EnemyStateFrozen<T> : State<T> {
        public override void Enter() {
            Log("FROZEN");
        }

        public override EStates GetStateType() {
            return EStates.Frozen;
        }
    }
}