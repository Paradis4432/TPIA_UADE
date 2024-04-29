using System;
using Unity.VisualScripting;

namespace trees.impls {
    public class ConditionalNode : AbstractTree {
        private Func<bool> _condition;
        private ActionableNode _trueNode;
        private ActionableNode _falseNode;

        public ConditionalNode(Func<bool> condition, ActionableNode trueNode, ActionableNode falseNode) {
            if (condition == null || trueNode == null || falseNode == null) throw new ArgumentNullException();

            _condition = condition;
            _trueNode = trueNode;
            _falseNode = falseNode;
        }


        public override void Execute() {
            if (_condition()) _trueNode.Execute();
            else _falseNode.Execute();
        }

        public bool IsValid() {
            return _condition();
        }
    }
}