using System;

namespace trees.impls {
    public class ActionableNode : AbstractTree {
        private readonly Action _action;

        public ActionableNode(Action action) {
            this._action = action;
        }

        public override void Execute() {
            this._action();
        }
    }
}