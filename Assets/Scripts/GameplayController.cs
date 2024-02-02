using System;
using Cards;
using CustomTimer;
using UnityEngine;
using Zenject;

public class GameplayController : MonoBehaviour
{ 
    [SerializeField] private CardsRepository cardsRepository;

    [Inject] private CardLine _cardLine;
        
    private CardCreatorProcessor _cardCreatorProcessor;

    private Timer _timer;

    private void Awake()
    {
        _timer = new Timer(2);
        _timer.OnTimerEnd += SpawnRandomEnemy;
        
        _cardCreatorProcessor = new CardCreatorProcessor(cardsRepository, _cardLine);
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
}