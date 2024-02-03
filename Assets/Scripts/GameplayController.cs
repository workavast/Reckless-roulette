using Cards;
using CustomTimer;
using GameCycle;
using Managers;
using UnityEngine;
using Zenject;

public class GameplayController : MonoBehaviour
{ 
    [SerializeField] private CardsRepository cardsRepository;

    [Inject] private CardLine _cardLine;
    [Inject] private PlayerHero _playerHero;
    [Inject] private PathManager _pathManager;
    [Inject] private GameCycleController _gameCycleController;
        
    private CardCreatorProcessor _cardCreatorProcessor;

    private Timer _timer;

    private void Awake()
    {
        _timer = new Timer(2);
        _timer.OnTimerEnd += SpawnRandomEnemy;
        
        _cardCreatorProcessor = new CardCreatorProcessor(cardsRepository, _cardLine);

        _playerHero.OnDie += LooseGame;
        _cardLine.OnFillLine += LooseGame;
        _pathManager.OnArriveDestination += CompleteGame;
    }

    private void Update()
    {
        _timer.Tick(Time.deltaTime);
    }

    private void SpawnRandomEnemy()
    {
        _timer.Reset();
        _cardCreatorProcessor.CreateRandomCard();
    }

    private void LooseGame()
    {
        _timer.SetPause();
        _gameCycleController.SwitchState(GameCycleState.Pause);
        Debug.Log("you loosed");
    }

    private void CompleteGame()
    {
        _timer.SetPause();
        _gameCycleController.SwitchState(GameCycleState.Pause);
        Debug.Log("you wined");
    }
}