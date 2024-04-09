using EventBusExtension;
using Events;
using GameCycle;
using Managers;
using TrainLevel;
using UI_System;
using UI_System.Elements;
using UnityEngine;
using Zenject;

public class TrainGameplayController : MonoBehaviour, IEventReceiver<BossDie>
{
    [SerializeField] private TrainWindow startWindow; 
    [SerializeField] private TrainWindow enemyCardWindow; 
    [SerializeField] private TrainWindow effectCardWindow; 
    [SerializeField] private TrainWindow levelUpWindow; 
    [SerializeField] private TrainWindow healCardWindow; 
    [SerializeField] private TrainWindow pathWindow;
    
    [Inject] private EventBus _eventBus;
    [Inject] private UI_Controller _uiController;
    [Inject] private CardLine _cardLine;
    [Inject] private PlayerHero _playerHero;
    [Inject] private PathManager _pathManager;
    [Inject] private GameCycleController _gameCycleController;

    public ReceiverIdentifier ReceiverIdentifier { get; } = new();

    private TrainProcessor _trainProcessor;
    
    private void Awake()
    {
        _eventBus.Subscribe(this);
        
        _playerHero.OnDie += LooseGame;

        _trainProcessor = new TrainProcessor(_gameCycleController, _cardLine, startWindow, enemyCardWindow,
            effectCardWindow, levelUpWindow, healCardWindow, pathWindow);
    }

    private void Start()
    {
        _trainProcessor.Invoke();
    }
    
    public void OnEvent(BossDie t) => CompleteGame();
    
    private void LooseGame()
    {
        _gameCycleController.SwitchState(GameCycleState.Pause);
        _uiController.SetScreen(ScreenType.GameplayLoose);
        PlayerHeroSaver.Instance.Dispose();
    }

    private void CompleteGame()
    {
        _gameCycleController.SwitchState(GameCycleState.Pause);
        
        PlayerHeroSaver.Instance.SaveParams(_playerHero);
        _uiController.SetScreen(ScreenType.LocationWin);
    }

    private void OnDestroy()
    {
        _eventBus?.UnSubscribe(this);
    }
}