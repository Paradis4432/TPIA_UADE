namespace FSM.state {
    public interface IState<TE> {
        void Enter();
        void Execute();
        void Sleep();
        void Add(TE transition, IState<TE> state);
        void Remove(TE transition, IState<TE> state);
        void Remove(IState<TE> state);
        IState<TE> Get(TE transition);
        void SetFSM(FSM<TE> fsm);
    }
}