using UnityEngine;

namespace entities.player {
    public interface IPlayer : IBaseEntity {
        // attack? 
        void ChangeModel(IModel model);

        public interface IModel : IBaseEntity.IModel {
            public float Speed { get; set; }
            public Rigidbody Rigidbody { get; set; }
            public void Move(Vector3 dir);
        }

        public interface IView : IBaseEntity.IView {
            Rigidbody Rigidbody { get; set; }
            GameObject Body { get; set; }
            Animator Animator { get; set; }
            void LookDir(Vector3 dir);
            void Update();
        }
    }
}