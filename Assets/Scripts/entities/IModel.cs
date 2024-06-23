using UnityEngine;

namespace entities {
    public interface IModel {
        void Move(Vector3 dir);
        void Look(Vector3 dir);
    }
}