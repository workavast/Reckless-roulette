using System;
using System.Collections.Generic;
using Cards;
using GameCycle;
using UI_System.CardUi;
using UnityEngine;
using Zenject;

public class CardLine : MonoBehaviour, IGameCycleUpdate, IGameCycleEnter, IGameCycleExit
{
    [SerializeField] private CardHolder[] cardHolders;
    [SerializeField] private Transform cardSpawnPos;
    [SerializeField] private Transform cardParent;
    [SerializeField] private GameObject cardPrefab;

    [Inject] private DiContainer _container;
    [Inject] private IGameCycleController _gameCycleController;
    
    private readonly List<MovableCard> _movableCards = new();

    public event Action OnFillLine;

    private void Awake()
    {
        _gameCycleController.AddListener(GameCycleState.Gameplay, this as IGameCycleUpdate);
        _gameCycleController.AddListener(GameCycleState.Gameplay, this as IGameCycleEnter);
        _gameCycleController.AddListener(GameCycleState.Gameplay, this as IGameCycleExit);
    }

    public void GameCycleEnter()
    {
        foreach (var movableCard in _movableCards)
            movableCard.SwitchInteractionState(true);
    }
    
    public void GameCycleExit()
    {
        foreach (var movableCard in _movableCards)
            movableCard.SwitchInteractionState(false);
    }
    
    public void GameCycleUpdate()
    {
        foreach (var movableCard in _movableCards)
            movableCard.HandleUpdate(Time.deltaTime);
    }
    
    public void SpawnNewCard(CardConfigBase cardConfig)
    {           
        var cardProcessor = (CardProcessorBase)Activator.CreateInstance(cardConfig.CardProcessorBase.GetType());
        _container.Inject(cardProcessor);
        
        var movableCard = Instantiate(cardPrefab, cardParent).GetComponent<MovableCard>();
        movableCard.transform.position = cardSpawnPos.position;
        movableCard.SetCardData(cardProcessor, cardConfig.Sprite);
        movableCard.OnUse += RemoveCard;
        _movableCards.Add(movableCard);
        movableCard.OnReachDestination += CheckFillLine;

        for (int i = 0; i < cardHolders.Length; i++)
        {
            if (!cardHolders[i].HaveMovableCard)
            {
                cardHolders[i].SetCard();
                movableCard.SetDestination(i, cardHolders[i].transform);
                return;
            }
        }
        
        OnFillLine?.Invoke();
    }
    
    private void CheckFillLine()
    {
        if(_movableCards.Count < cardHolders.Length) return;
        
        bool allLineOccupied = true;
        foreach (var movableCard in _movableCards)
            if (!movableCard.IsReachDestination) allLineOccupied = false;
    
        if (allLineOccupied)
            OnFillLine?.Invoke();
    }

    private void RemoveCard(int holderIndex)
    {
        var movableCard = _movableCards[holderIndex];
        _movableCards.RemoveAt(holderIndex);
        cardHolders[holderIndex].ResetCard();
        Destroy(movableCard.gameObject);

        UpdateCardsDestinations(holderIndex);
    }

    private void UpdateCardsDestinations(int holderIndex)
    {
        foreach (var movableCard in _movableCards)
        {
            if (movableCard.HolderIndex > holderIndex)
            {
                cardHolders[movableCard.HolderIndex].ResetCard();
                var index = movableCard.HolderIndex - 1;
                cardHolders[index].SetCard();
                _movableCards[index].SetDestination(index, cardHolders[index].transform);
            }
        }
    }

    private void OnDestroy()
    {
        _gameCycleController.RemoveListener(GameCycleState.Gameplay, this as IGameCycleUpdate);
        _gameCycleController.AddListener(GameCycleState.Gameplay, this as IGameCycleEnter);
        _gameCycleController.AddListener(GameCycleState.Gameplay, this as IGameCycleExit);
    }
}
