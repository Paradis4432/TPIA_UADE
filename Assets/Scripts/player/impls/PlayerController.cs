using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace player.impls {
    [RequireComponent(typeof(IPlayerModel)), RequireComponent(typeof(IPlayerView))]
    public class PlayerController : MonoBehaviour {
        [NotNull] private IPlayerModel _player;
        [NotNull] private IPlayerView _view;

        // FSM
        private void Awake() {
            _player = GetComponent<IPlayerModel>();
            _view = GetComponent<IPlayerView>();
            // init FSM
        }

        public void ChangeModel(IPlayerModel model) {
            _player = model;
        }

        void InitFSM() {
            // TODO init
        }

        private void Update() {
            // TODO fsm update
        }
    }
}