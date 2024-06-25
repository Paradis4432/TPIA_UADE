using System;

namespace trees.impls {
    public class ActionNode : ITreeNode {
        private Action _action;

        public ActionNode(Action action) {
            _action = action;
        }

        public void Execute() {
            _action();
        }
    }
}