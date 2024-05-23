using fsm.impls;

namespace fsm {
    public interface IState<TState> {
        void Enter();
        void Execute();
        void Sleep();
        void Add(TState input, IState<TState> state);
        void Remove(TState input);
        void Remove(IState<TState> state);
        IState<TState> Get(TState input);
        Fsm<TState> SetFsm { get; set; }
    }
}