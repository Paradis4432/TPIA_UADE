using System.Collections.Generic;

namespace trees.impls {
    public class RandomNode : ITreeNode {
        private Dictionary<ITreeNode, float> _dic;

        public RandomNode(Dictionary<ITreeNode, float> dic) {
            _dic = dic;
        }

        public void Execute() {
            ITreeNode r = RandomOf.Roulette(_dic);
            r.Execute();
        }
    }
}