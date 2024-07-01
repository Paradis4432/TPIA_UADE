using System.Collections.Generic;
using DefaultNamespace;
using entities.enemies.states;
using entities.enemies.states.attacks;
using entities.player.impls;
using fsm;
using fsm.impls;
using fsm.states;
using los.impls;
using trees;
using trees.impls;
using UnityEngine;

namespace entities.enemies.impls {
    public class EnemyController : MonoBehaviour {
        public Player target;
        public int attackRange = 1;

        private LineOfSight _los;
        private Fsm<EStates> _fsm;
        private Enemy _model;
        private ITreeNode _treeNode;
        private int _idleCounter = 0;
        private bool _idling = false;

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
            EnemyStateDie<EStates> die = new();
            EnemyStateFrozen<EStates> freeze = new();
            EnemyStateMove<EStates> move = new(_model);


            EnemyStateAttackTiny<EStates> attackTiny = new();
            EnemyStateAttackMed<EStates> attackMed = new();
            EnemyStateAttackBig<EStates> attackBig = new();

            Connect(idle, new List<IState<EStates>> { chase, attackTiny, attackMed, attackBig, freeze, die, move });
            Connect(chase, new List<IState<EStates>> { idle, attackTiny, attackMed, attackBig, freeze, die, move });
            Connect(freeze, new List<IState<EStates>> { chase, idle, attackTiny, attackMed, attackBig, die, move });
            Connect(attackTiny, new List<IState<EStates>> { chase, idle, attackMed, attackBig, freeze, die, move });
            Connect(attackMed, new List<IState<EStates>> { chase, idle, attackTiny, attackBig, freeze, die, move });
            Connect(attackBig, new List<IState<EStates>> { chase, idle, attackTiny, attackMed, freeze, die, move });
            Connect(move, new List<IState<EStates>> { chase, idle, attackTiny, attackMed, attackBig, freeze, die });

            _fsm = new Fsm<EStates>(idle);
        }

        private static void Connect(IState<EStates> source, IEnumerable<IState<EStates>> targets) {
            foreach (IState<EStates> state in targets) {
                if (state.GetStateType().Equals(source.GetStateType()))
                    continue;
                source.Add(state.GetStateType(), state);
            }
        }

        private void SetupTree() {
            ActionNode idle = new(() => _fsm.Transition(EStates.Idle));
            ActionNode die = new(() => _fsm.Transition(EStates.Die));
            ActionNode chase = new(() => _fsm.Transition(EStates.Chase));
            ActionNode frozen = new(() => _fsm.Transition(EStates.Frozen));
            ActionNode move = new(() => _fsm.Transition(EStates.Move));
            ActionNode attackTiny = new(() => _fsm.Transition(EStates.AttackTiny));
            ActionNode attackMed = new(() => _fsm.Transition(EStates.AttackMed));
            ActionNode attackBig = new(() => _fsm.Transition(EStates.AttackBig));

            Dictionary<ITreeNode, float> d = new() {
                [attackTiny] = 70,
                [attackMed] = 40,
                [attackBig] = 10
            };
            RandomNode randomNode = new(d);

            QuestionNode idleOrMode = new(() => {
                if (++_idleCounter < 1000) return _idling;
                _idleCounter = 0;
                _idling = !_idling;

                return _idling;
            }, move, idle);
            QuestionNode inRange = new(TargetInRange, chase, idleOrMode);
            QuestionNode inCloseRange =
                new(() => _los.CheckDistance(target.transform) <= attackRange, randomNode, inRange);
            QuestionNode isFrozen = new(() => GameManager.Instance.EnemiesFrozen, frozen, inCloseRange);
            QuestionNode isDead = new(() => _model.hp <= 0, die, isFrozen);

            _treeNode = isDead;
        }

        private bool TargetInRange() {
            return
                _los.CheckRange(target
                    .transform) && // todo check range and check distance both call Vector3.Distance that calls math.sqrt. fix this
                /*_los.CheckAngle(target.transform) &&*/
                // bug when it changes the angle to move to the target using the nodes, check angle stops working due to the change in the direction
                _los.CheckView(target.transform);
        }
    }
}