using System;
using System.Collections.Generic;
using System.Linq;
using EnumValuesExtension;
using UnityEngine;

namespace GameCycle
{
    [DefaultExecutionOrder(-10000)]
    public class GameCycleController : MonoBehaviour, IGameCycleController, IGameCycleSwitcher
    {
        [SerializeField] private GameCycleState startState;
        
        private readonly Dictionary<GameCycleState, Dictionary<GameCycleInvokeType, Action>> _actions = new ();

        public GameCycleState CurrentState { get; private set; }

        private void Awake()
        {
            CurrentState = startState;
            var gameStatesTypes = EnumValuesTool.GetValues<GameCycleState>();
            var gameCycleInvokeTypes = EnumValuesTool.GetValues<GameCycleInvokeType>().ToList();
            foreach (var state in gameStatesTypes)
            {
                _actions.Add(state, new Dictionary<GameCycleInvokeType, Action>());
                foreach (var invokeType in gameCycleInvokeTypes)
                    _actions[state].Add(invokeType, null);
            }
        }

        private void Update() => _actions[CurrentState][GameCycleInvokeType.Update]?.Invoke();
        private void FixedUpdate() => _actions[CurrentState][GameCycleInvokeType.FixedUpdate]?.Invoke();

        public void AddListener(GameCycleState state, IGameCycleUpdate iGameCycleUpdate)
            => AddListener(state, GameCycleInvokeType.Update, iGameCycleUpdate.GameCycleUpdate);
        public void AddListener(GameCycleState state, IGameCycleFixedUpdate iGameCycleFixedUpdate) 
            => AddListener(state, GameCycleInvokeType.FixedUpdate, iGameCycleFixedUpdate.GameCycleFixedUpdate);
        public void AddListener(GameCycleState state, IGameCycleEnter iGameCycleEnter)
            => AddListener(state, GameCycleInvokeType.StateEnter, iGameCycleEnter.GameCycleEnter);
        public void AddListener(GameCycleState state, IGameCycleExit iGameCycleExit)
            => AddListener(state, GameCycleInvokeType.StateExit, iGameCycleExit.GameCycleExit);
        
        private void AddListener(GameCycleState state, GameCycleInvokeType invokeType, Action method)
        {
            _actions[state][invokeType] -= method;
            _actions[state][invokeType] += method;
        }

        public void RemoveListener(GameCycleState state, IGameCycleUpdate iGameCycleUpdate) 
            => _actions[state][GameCycleInvokeType.Update] -= iGameCycleUpdate.GameCycleUpdate;
        public void RemoveListener(GameCycleState state, IGameCycleFixedUpdate iGameCycleFixedUpdate) 
            => _actions[state][GameCycleInvokeType.FixedUpdate] -= iGameCycleFixedUpdate.GameCycleFixedUpdate;
        public void RemoveListener(GameCycleState state, IGameCycleEnter iGameCycleEnter)
            => _actions[state][GameCycleInvokeType.StateEnter] -= iGameCycleEnter.GameCycleEnter;
        public void RemoveListener(GameCycleState state, IGameCycleExit iGameCycleExit)
            => _actions[state][GameCycleInvokeType.StateExit] -= iGameCycleExit.GameCycleExit;

        public void SwitchState(GameCycleState newCycleState)
        {
            if(CurrentState == newCycleState) return;
            
            _actions[CurrentState][GameCycleInvokeType.StateExit]?.Invoke();
            CurrentState = newCycleState;
            _actions[CurrentState][GameCycleInvokeType.StateEnter]?.Invoke();
        }
    }
}