using UnityEngine;

namespace entities {
    public interface IBaseEntity {
        void Awake();
        void Update();

        public interface IModel {
        }

        public interface IView {

        }
    }
}