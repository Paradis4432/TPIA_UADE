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
        private Enemy _enemy;
        private Player _target;

        public EnemyStateChase(Enemy enemy, Player target) {
            _enemy = enemy;
            _target = target;
        }

        public override void Execute() {
            /*Vector3 dir = (_target.position - _enemy.transform.position).normalized;
            _enemy.Move(dir);
            _enemy.Look(dir);*/

            if (!GridManager.Cache.ContainsKey(_enemy)) return;
            if (!GridManager.Cache.ContainsKey(_target)) return;

            IPoint start = GridManager.Cache[_enemy];
            IPoint end = GridManager.Cache[_target];

            List<IPoint> points = Bfs.Run(
                start,
                point => point == end);

            if (points.Count <= 1) {
                Debug.Log("no path found");
                return;
            }

            Vector3 newPos = points[1].GetPosition();
            Vector3 position = _enemy.transform.position;
            newPos.y = position.y;

            //newPos.Set(newPos.x, transform.position.y, newPos.z);
            //Debug.Log(newPos);

            Vector3 dir = (newPos - position).normalized;
            Debug.Log(dir);
            _enemy.Move(dir);
            _enemy.Look(dir);
        }

        public override void Enter() {
            Debug.Log("CHASE");
        }
    }
}