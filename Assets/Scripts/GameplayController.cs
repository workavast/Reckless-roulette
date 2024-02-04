using Cards;
using CustomTimer;
using GameCycle;
using Managers;
using UI_System;
using UI_System.Elements;
using UnityEngine;
using Zenject;

public class GameplayController : MonoBehaviour
{ 
    [SerializeField] private LocationCardsConfig locationCardsConfig;
    
    [Inject] private UI_Controller _uiController;
    [Inject] private CardLine _cardLine;
    [Inject] private PlayerHero _playerHero;
    [Inject] private PathManager _pathManager;
    [Inject] private GameCycleController _gameCycleController;
        
    private CardCreatorProcessor _cardCreatorProcessor;

    private Timer _spawnTimer;

    private void Awake()
    {
        _spawnTimer = new Timer(2);
        _spawnTimer.OnTimerEnd += SpawnRandomEnemy;
        
        _cardCreatorProcessor = new CardCreatorProcessor(locationCardsConfig, _cardLine);

        _playerHero.OnDie += LooseGame;
        _cardLine.OnFillLine += LooseGame;
        _pathManager.OnArriveDestination += CompleteGame;
    }

    private void Update()
    {
        _spawnTimer.Tick(Time.deltaTime);
    }

    private void SpawnRandomEnemy()
    {
        _spawnTimer.Reset();
        _cardCreatorProcessor.CreateRandomCard();
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
}