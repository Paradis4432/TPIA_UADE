using entities.enemies.impls;
using fsm.states.impls;
using UnityEngine;

namespace entities.enemies.states {
    public class EnemyStateChase<T> : State<T> {
        private Enemy _enemy;
        private Transform _target;

        public EnemyStateChase(Enemy enemy, Transform target) {
            _enemy = enemy;
            _target = target;
        }

        public override void Execute() {
            base.Execute();

            Vector3 dir = (_target.position - _enemy.transform.position).normalized;
            _enemy.Move(dir);
            _enemy.Look(dir);
        }
        
        public override void Enter() {
            Debug.Log("CHASE");
        }
    }
}