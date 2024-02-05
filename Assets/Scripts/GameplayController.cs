using Cards;
using CustomTimer;
using EventBusExtension;
using Events;
using GameCycle;
using Managers;
using UI_System;
using UI_System.Elements;
using UnityEngine;
using Zenject;

public class GameplayController : MonoBehaviour, IEventReceiver<BossDie>
{ 
    [SerializeField] private LocationCardsConfig locationCardsConfig;

    [Inject] private EventBus _eventBus;
    [Inject] private UI_Controller _uiController;
    [Inject] private CardLine _cardLine;
    [Inject] private PlayerHero _playerHero;
    [Inject] private PathManager _pathManager;
    [Inject] private GameCycleController _gameCycleController;
        
    private CardCreatorProcessor _cardCreatorProcessor;
    private Timer _spawnTimer;
    
    public ReceiverIdentifier ReceiverIdentifier { get; } = new();

    private void Awake()
    {
        _eventBus.Subscribe(this);
        
        _spawnTimer = new Timer(2);
        _spawnTimer.OnTimerEnd += CreateCard;
        
        _cardCreatorProcessor = new CardCreatorProcessor(locationCardsConfig, _cardLine);

        _playerHero.OnDie += LooseGame;
        _cardLine.OnFillLine += LooseGame;
        _pathManager.OnArriveDestination += ArriveDestination;
    }

    private void Update()
    {
        _spawnTimer.Tick(Time.deltaTime);
    }

    public void OnEvent(BossDie t) => CompleteGame();
    
    private void CreateCard()
    {
        _spawnTimer.Reset();
        _cardCreatorProcessor.CreateRandomCard();
    }

    private void ArriveDestination()
    {
        _spawnTimer.SetPause();
        _cardCreatorProcessor.CreateBossCard();
    }
    
    private void LooseGame()
    {
        _spawnTimer.SetPause();
        _gameCycleController.SwitchState(GameCycleState.Pause);
        _uiController.SetScreen(ScreenType.GameplayLoose);
    }

    private void CompleteGame()
    {
        _spawnTimer.SetPause();
        _gameCycleController.SwitchState(GameCycleState.Pause);
        _uiController.SetScreen(ScreenType.GameplayWin);
    }

    private void OnDestroy()
    {
        _eventBus.UnSubscribe(this);
    }
}