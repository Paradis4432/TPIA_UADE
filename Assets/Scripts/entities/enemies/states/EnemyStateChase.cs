using System.Collections.Generic;
using entities.enemies.impls;
using entities.player.impls;
using fsm.states;
using fsm.states.impls;
using grid;
using grid.pathfinding;
using grid.points;
using UnityEngine;

namespace entities.enemies.states {
    public class EnemyStateChase<T> : State<T> {
        private readonly Enemy _enemy;
        private readonly Player _target;
        private List<IPoint> _points;

        public EnemyStateChase(Enemy enemy, Player target) {
            _enemy = enemy;
            _target = target;

            GridManager.RegisterListener(_target, ResetPath);
            /*GridManager.RegisterListener(_enemy, () => {
             // bug: not always removing the first
                if (_points is { Count: > 0 }) _points.RemoveAt(0);
            });*/
        }

        public void ResetPath() {
            _points = null;
        }

        public override void Execute() {
            if (!GridManager.IsEntityCached(_enemy)) return;
            IPoint c = GridManager.GetPointForEntity(_enemy);
            if (_points == null) {
                if (!GridManager.IsEntityCached(_target)) return;

                _points = Algos.Bfs(c, point =>
                        point == GridManager.GetPointForEntity(_target)
                    /*(p0) => p0.Weight,
                    (p0) => p0.Heuristic*/
                );
            }

            if (_points.Count <= 1) return;

            if (c == _points[1] || c == _points[0]) {
                // bug, it might be concurrency with points setting to null and the 
                // enter trigger not removing the first 
                // [0] is prev and [1] is current causing enemy to spin in place
                _points.RemoveAt(0);
            }

            if (_points.Count <= 1) return;

            Vector3 newPos = _points[1].GetPosition();
            Vector3 position = _enemy.transform.position;
            newPos.y = position.y;

            Vector3 dir = (newPos - position).normalized;
            _enemy.Move(dir);
            _enemy.Look(dir);
        }

        public override EStates GetStateType() {
            return EStates.Chase;
        }

        public override void Enter() {
            Log("CHASE");
        }
    }
}