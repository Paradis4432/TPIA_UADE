using entities.enemies.impls;
using fsm.states;
using fsm.states.impls;
using UnityEngine;

namespace entities.enemies.states {
    public class EnemyStateIdle<T> : State<T> {
        private readonly Enemy _enemy;

        public EnemyStateIdle(Enemy enemy) {
            _enemy = enemy;
        }

        public override void Enter() {
            Log("IDLE");
        }

        public override void Execute() {
            _enemy.transform.Rotate(0, 0.2f, 0);
        }

        public override EStates GetStateType() {
            return EStates.Idle;
        }
    }
}