namespace GameCycle
{
    public interface IGameCycleController
    {
        public GameCycleState CurrentState { get; }
        
        public void AddListener(GameCycleState state, IGameCycleUpdate iGameCycleUpdate);
        public void AddListener(GameCycleState state, IGameCycleFixedUpdate iGameCycleFixedUpdate);
        public void AddListener(GameCycleState state, IGameCycleEnter iGameCycleEnter);
        public void AddListener(GameCycleState state, IGameCycleExit iGameCycleExit); 

        public void RemoveListener(GameCycleState state, IGameCycleUpdate iGameCycleUpdate);
        public void RemoveListener(GameCycleState state, IGameCycleFixedUpdate iGameCycleFixedUpdate);
        public void RemoveListener(GameCycleState state, IGameCycleEnter iGameCycleEnter);
        public void RemoveListener(GameCycleState state, IGameCycleExit iGameCycleExit);
    }
}