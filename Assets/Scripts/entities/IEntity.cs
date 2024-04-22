using UnityEngine;

namespace entities {
    public interface IEntity {
        int HP { get; set; }
        float Speed { get; set; }
        Vector3 getPosition();
    }
}