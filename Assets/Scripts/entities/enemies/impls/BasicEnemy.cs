using entities.enemies.impls.states;
using entities.view;
using FSM;
using UnityEngine;

namespace entities.enemies.impls {
    public class BasicEnemy : MonoBehaviour, IEnemy {
        [SerializeField] private Transform target;
        
        private ILineOfSight _lineOfSight;
        private readonly FSM<EEnemyStates> _fsm = new();

        public void Awake() {
            this._lineOfSight = this.GetComponent<ILineOfSight>();
            // alert?
            
            BasicEnemyStates<EEnemyStates>.Patrol<EEnemyStates> patrol = new(EEnemyStates.PATROL);
            BasicEnemyStates<EEnemyStates>.Idle<EEnemyStates> idle = new(EEnemyStates.IDLE);
            BasicEnemyStates<EEnemyStates>.Attack<EEnemyStates> attack = new(EEnemyStates.ATTACK);
            
            patrol.Add(EEnemyStates.IDLE, idle);
            idle.Add(EEnemyStates.PATROL, patrol);
            idle.Add(EEnemyStates.ATTACK, attack);
            
            this._fsm.SetState(patrol);
        }

        public void Update() {
            this._fsm.Update();

            if (this.target == null) return;

            if (
                this._lineOfSight.CheckRange(this.target) &&
                this._lineOfSight.CheckAngle(this.target) &&
                this._lineOfSight.CheckView(this.target)
            ) {
                // attack or do smth
                
            }
            
        }

        private class BEModel : IBaseEntity.IModel {
        }

        private class BEView : IBaseEntity.IView {
        }
    }
}