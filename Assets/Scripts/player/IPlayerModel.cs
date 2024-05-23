using UnityEngine;

namespace player {
    public interface IPlayerModel {
        void Move(Vector3 dir);
        void Look(Vector3 dir);
    }
}