using UnityEngine;

namespace trees.impls {
    public class ComplexNode<T> where T : ITree {
        public static class test {
            static void a() {
                new ConditionalNode(() => { return true; },
                    new ActionableNode(() => {
                        new ConditionalNode(() => { return false; }, new ActionableNode(() => { }),
                            new ActionableNode(() => { Debug.Log("test"); }));
                    }), new ActionableNode(() => {
                        new ConditionalNode(() => false, new ActionableNode(() => { }), new ActionableNode(() => { }));
                    }));
            }

            static bool cond0() {
                return true;
            }

            static bool cond1() {
                return false;
            }
        }


        /*
         * start -> complexNode
         * has a condition ->
         * true -> complexNode
         * false ->  complexNode
         */
    }
}