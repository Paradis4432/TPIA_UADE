using entities.enemies.states;
using entities.player.impls;
using fsm.impls;
using fsm.states;
using los.impls;
using trees;
using trees.impls;
using UnityEngine;

namespace entities.enemies.impls {
    public class EnemyController : MonoBehaviour {
        public Player target;

        private LineOfSight _los;
        private Fsm<EStates> _fsm;
        private Enemy _model;
        private ITreeNode _treeNode;

        private void Awake() {
            _model = GetComponent<Enemy>();
            _los = GetComponent<LineOfSight>();
        }

        private void Start() {
            SetupFsm();
            SetupTree();
        }

        private void Update() {
            _fsm.Update();
            _treeNode.Execute();
        }

        private void SetupFsm() {
            EnemyStateIdle<EStates> idle = new(_model);
            EnemyStateChase<EStates> chase = new(_model, target);
            EnemyStateAttack<EStates> attack = new();
            EnemyStateDie<EStates> die = new();

            idle.Add(EStates.Chase, chase);
            idle.Add(EStates.Attack, attack);
            idle.Add(EStates.Die, die);

            chase.Add(EStates.Attack, attack);
            chase.Add(EStates.Die, die);
            chase.Add(EStates.Idle, idle);

            attack.Add(EStates.Chase, chase);
            attack.Add(EStates.Die, die);
            attack.Add(EStates.Idle, idle);

            die.Add(EStates.Idle, idle);
            die.Add(EStates.Chase, chase);
            die.Add(EStates.Attack, attack);

            _fsm = new Fsm<EStates>(chase);
        }

        private void SetupTree() {
            ActionNode idle = new(() => _fsm.Transition(EStates.Idle));
            ActionNode die = new(() => _fsm.Transition(EStates.Die));
            ActionNode attack = new(() => _fsm.Transition(EStates.Attack));
            ActionNode chase = new(() => _fsm.Transition(EStates.Chase));


            QuestionNode targetInRange = new(TargetInRange, chase, idle);
            QuestionNode isAlive = new(() => _model.hp > 0, targetInRange, die);

            // if enemy is activated
            // should find path -> if path == null || last IPoint of target has changed in GridManager.Cache
            // if should find path change to exploring state
            // else if target in range -> change to chase state to go directly to the target
            // else target not in range -> change to idle

            _treeNode = isAlive;
        }

        private bool TargetInRange() {
            return _los.CheckRange(target.transform) /*&&
                   _los.CheckAngle(target.transform) &&
                   _los.CheckView(target.transform)*/;
        }
    }
}