using UnityEngine;

namespace entities.player {
    public interface IPlayer : IBaseEntity {
        // attack? 
        void ChangeModel(IModel model);
        Rigidbody Rigidbody { get; set; }
        GameObject Body { get; set; }
        Animator Animator { get; set; }


        public interface IModel : IBaseEntity.IModel {
            public float Speed { get; set; }
            public IPlayer Parent { get; set; }
            public void Move(Vector3 dir);
        }

        public interface IView : IBaseEntity.IView {
            public IPlayer Parent { get; set; }

            void LookDir(Vector3 dir);
            void Update();
        }
    }
}