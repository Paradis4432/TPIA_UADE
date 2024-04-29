using UnityEngine;

namespace entities {
    public interface IBaseEntity {
        void Awake();
        void Update();

        public interface IModel {
            public Transform Transform { get; set; }
            void Move(Vector3 dirNormalized);
            void LookDir(Vector3 dirNormalized);
        }

        public interface IView {

        }
    }
}