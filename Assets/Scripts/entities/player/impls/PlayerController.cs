using System.Diagnostics.CodeAnalysis;
using entities.player.states;
using fsm.impls;
using fsm.states;
using UnityEngine;

namespace entities.player.impls {
    public class PlayerController : MonoBehaviour {
        [NotNull] private IPlayerModel _player;
        [NotNull] private IPlayerView _view;

        [NotNull] private Fsm<EStates> _fsm;

        private void Awake() {
            _player = GetComponent<IPlayerModel>();
            _view = GetComponent<IPlayerView>();

            InitFsm();
        }

        public void ChangeModel(IPlayerModel model) {
            _player = model;
        }

        private void InitFsm() {
            _fsm = new Fsm<EStates>();

            PStateIdle<EStates> idle = new(EStates.Walk);
            PStateWalk<EStates> walk = new(_player, _view, EStates.Idle);

            walk.Add(EStates.Idle, idle);
            idle.Add(EStates.Walk, walk);

            _fsm.SetInit(idle);
        }

        private void Update() {
            _fsm.Update();
        }
    }
}