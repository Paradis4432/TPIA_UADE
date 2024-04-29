using entities.enemies.impls.states;
using entities.view;
using FSM;
using trees;
using trees.impls;
using UnityEngine;

namespace entities.enemies.impls {
    public class BasicEnemy : MonoBehaviour, IEnemy {
        [SerializeField] private Transform target;
        public Rigidbody Rigidbody { get; set; }
        public IBaseEntity.IModel Model { get; set; }
        public Transform Transform { get; set; }
        public Animator Animator { get; set; }

        private ILineOfSight _lineOfSight;
        private FSM<EEnemyStates> _fsm;
        private ITree _root;

        public void Awake() {
            _lineOfSight = GetComponent<ILineOfSight>();
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();

            Model = new BEModel(this);
            Transform = transform;
        }

        private void Start() {
            SetupFSM();
            SetupTree();
        }

        private void SetupFSM() {
            BasicEnemyStates<EEnemyStates>.Patrol<EEnemyStates> patrol = new(EEnemyStates.PATROL, this);
            BasicEnemyStates<EEnemyStates>.Idle<EEnemyStates> idle = new(EEnemyStates.IDLE, this);
            BasicEnemyStates<EEnemyStates>.Attack<EEnemyStates> attack = new(EEnemyStates.ATTACK, this);

            patrol.Add(EEnemyStates.IDLE, idle);
            patrol.Add(EEnemyStates.ATTACK, attack);

            idle.Add(EEnemyStates.PATROL, patrol);
            idle.Add(EEnemyStates.ATTACK, attack);

            attack.Add(EEnemyStates.PATROL, patrol);
            attack.Add(EEnemyStates.IDLE, idle);

            _fsm = new FSM<EEnemyStates>(idle, target);
        }

        private void SetupTree() {
            ActionableNode idle = new(() => _fsm.Transition(EEnemyStates.IDLE));
            ActionableNode patrol = new(() => _fsm.Transition(EEnemyStates.PATROL));
            ActionableNode attack = new(() => _fsm.Transition(EEnemyStates.ATTACK));

            ConditionalNode qInRange = new(InRange, attack, patrol);
            /*ConditionalNode qShouldIdle = new(() => {
                //TODO is pasaron 10 segundos, de alguna manera o usar random para que no sea tan predecible
                return false;
            }, idle, patrol);
            ConditionalNode qInCooldown = new(() => {
                // TODO agregar cooldown a los ataques de los enemigos
                return false;
            }, null, null);*/

            _root = qInRange;
        }

        private bool InRange() {
            return _lineOfSight.CheckRange(target) &&
                   _lineOfSight.CheckAngle(target) &&
                   _lineOfSight.CheckView(target);
        }

        public void Update() {
            _fsm.Update();
            _root.Execute();

            _fsm.Transition(InRange() ? EEnemyStates.ATTACK : EEnemyStates.PATROL);

            /*_fsm.Transition(EEnemyStates.ATTACK);
            _fsm.SetTarget(target);
            // idle o patrol
            _fsm.Transition(EEnemyStates.PATROL);*/
        }

        private class BEModel : IBaseEntity.IModel {
            public float Speed { get; set; } = 5f;
            public IEnemy Parent { get; set; }

            public BEModel(IEnemy parent) {
                Parent = parent;
            }

            public Transform Transform {
                get => Parent.Transform;
                set => Parent.Transform = value;
            }

            public void Move(Vector3 dir) {
                dir *= Speed;
                dir.y = Parent.Rigidbody.velocity.y;
                Parent.Rigidbody.velocity = dir;
            }

            public void LookDir(Vector3 dirNormalized) {
            }
        }

        private class BEView : IBaseEntity.IView {
        }
    }
}