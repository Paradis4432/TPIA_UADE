using System;
using Unity.VisualScripting;

namespace trees.impls {
    public class ConditionalNode : AbstractTree {
        private Func<bool> _condition;
        private ActionableNode _trueNode;
        private ActionableNode _falseNode;

        public ConditionalNode(Func<bool> condition, ActionableNode trueNode, ActionableNode falseNode) {
            if (condition == null || trueNode == null || falseNode == null) throw new ArgumentNullException();

            this._condition = condition;
            this._trueNode = trueNode;
            this._falseNode = falseNode;
        }

        public override void Execute() {
            if (this._condition()) this._trueNode.Execute();
            else this._falseNode.Execute();
        }

        public bool IsValid() {
            return this._condition();
        }
    }
}