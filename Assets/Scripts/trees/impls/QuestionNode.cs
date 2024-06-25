using System;

namespace trees.impls {
    public class QuestionNode : ITreeNode {
        private Func<bool> _q;
        private ITreeNode _validNode;
        private ITreeNode _invalidNode;

        public QuestionNode(Func<bool> q, ITreeNode validNode, ITreeNode invalidNode) {
            _q = q;
            _validNode = validNode;
            _invalidNode = invalidNode;
        }

        public void Execute() {
            if (_q()) _validNode.Execute();
            else _invalidNode.Execute();
        }
    }
}