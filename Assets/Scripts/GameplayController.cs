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

    private bool _bossCardSpawned;
    private CardCreatorProcessor _cardCreatorProcessor;
    private Timer _spawnTimer;

    [SerializeField] private float minTime; 
    [SerializeField] private float maxTime; 
    
    public ReceiverIdentifier ReceiverIdentifier { get; } = new();

    private void Awake()
    {
        _eventBus.Subscribe(this);
        
        var newTime = Random.Range(minTime, maxTime);
        _spawnTimer = new Timer(newTime);
        _spawnTimer.OnTimerEnd += CreateCard;
        
        _cardCreatorProcessor = new CardCreatorProcessor(locationCardsConfig, _cardLine);

        _playerHero.OnDie += LooseGame;
        _cardLine.OnFillLine += WaitWhenCardLineHaveFreePlace;
        _cardLine.OnRemoveCard += TryContinueSpawnTimer;
        _pathManager.OnArriveDestination += ArriveDestination;
    }

    private void Update()
    {
        _spawnTimer.Tick(Time.deltaTime);
    }

    public void OnEvent(BossDie t) => CompleteGame();
    
    private void CreateCard()
    {
        var newTime = Random.Range(minTime, maxTime);
        _spawnTimer.SetMaxValue(newTime);
        _spawnTimer.Reset();
        _cardCreatorProcessor.CreateRandomCard();
    }

    private void ArriveDestination()
    {
        _bossCardSpawned = true;
        _spawnTimer.SetPause();
        _cardCreatorProcessor.CreateBossCard();
    }

    private void WaitWhenCardLineHaveFreePlace() => _spawnTimer.SetPause();

    private void TryContinueSpawnTimer()
    {
        if(!_cardLine.IsFull && !_bossCardSpawned)
            _spawnTimer.Continue();
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