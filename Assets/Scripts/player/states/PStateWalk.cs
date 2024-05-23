using fsm.states.impls;
using UnityEngine;

namespace player.states {
    public class PStateWalk<T> : State<T> {
        private readonly IPlayerModel _player;
        private IPlayerModel _view;
        private readonly T _idle;

        public PStateWalk(IPlayerModel player, IPlayerModel view, T idle) {
            _player = player;
            _view = view;
            _idle = idle;
        }

        public override void Execute() {
            base.Execute();
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            Vector3 dir = new Vector3(x, 0, y).normalized;
            _player.Move(dir);
            _player.Look(dir);

            if (dir is { x: 0, z: 0 }) {
                Fsm.Transition(_idle);
            }
        }
    }
}