using System.Collections.Generic;
using entities.enemies.impls;
using entities.player.impls;
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

            GridManager.RegisterListener(_target, () => _points = null);
            /*GridManager.RegisterListener(_enemy, () => {
             // bug: not always removing the first
                if (_points is { Count: > 0 }) _points.RemoveAt(0);
            });*/
        }

        public override void Execute() {
            if (!GridManager.IsEntityCached(_enemy)) return;
            IPoint c = GridManager.GetPointForEntity(_enemy);
            if (_points == null) {
                if (!GridManager.IsEntityCached(_target)) return;

                _points = Bfs.Run(
                    c,
                    point => point == GridManager.GetPointForEntity(_target));
            }

            if (_points.Count <= 1) {
                // Debug.Log("no path found");
                return;
            }

            if (c == _points[1] || c == _points[0]) {
                // bug, it might be concurrency with points setting to null and the 
                // enter trigger not removing the first 
                // [0] is prev and [1] is current causing enemy to spin in place
                _points.RemoveAt(0);
            }

            Vector3 newPos = _points[1].GetPosition();
            Vector3 position = _enemy.transform.position;
            newPos.y = position.y;

            Vector3 dir = (newPos - position).normalized;
            _enemy.Move(dir);
            _enemy.Look(dir);
        }

        public override void Enter() {
            Debug.Log("CHASE");
        }
    }
}