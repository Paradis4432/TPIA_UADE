using entities.view;
using UnityEngine;

namespace entities.enemies.impls {
    public class BasicEnemy : MonoBehaviour, IEnemy {
        [SerializeField] private Transform target;
        private ILineOfSight _lineOfSight;

        public void Awake() {
            this._lineOfSight = this.GetComponent<ILineOfSight>();
            // alert?
        }

        public void Update() {
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