using UnityEngine;

namespace entities.enemies {
    public interface IEnemy : IBaseEntity{
        Rigidbody Rigidbody { get; set; }
        
        IEnemy.IModel Model { get; set; }
        Transform Transform { get; set; }
        Animator Animator  { get; set; }
    }
}