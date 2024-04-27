using System;
using trees.impls;

namespace trees {
    public class TreeBuilder {
        
        
        
        public void If(ConditionalNode condition, ActionableNode action) {
            
        }

        public void IfElse(ConditionalNode condition, ActionableNode ctrue, ActionableNode cfalse) {
            if (condition == null || ctrue == null)
                throw new ArgumentException();
        }

        public void Run() {
            
            
            /**
             * if deadCondition
             *  if allDeadCondition
             *      action
             * else
             *  action
             */
        }
    }


    internal class Node {
        private Node _left;
        private Node _right;

        // if condition != null run action
        // else check condition and return
        private ConditionalNode _conditionalNode;
        private ActionableNode _actionableNode;
        
        public Node(Node left, Node right) {
            this._left = left;
            this._right = right;
        }

        public void run() {
            if (this._conditionalNode == null) {
                this._actionableNode.Execute();
                ;
            }
            else {
                if (this._conditionalNode) {
                    
                }
            }
        }
    }
}