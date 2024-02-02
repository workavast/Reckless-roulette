using System;
using System.Collections.Generic;
using Cards;
using UI_System.CardUi;
using UnityEngine;
using Zenject;

public class CardLine : MonoBehaviour
{
    [SerializeField] private CardHolder[] cardHolders;
    [SerializeField] private Transform cardSpawnPos;
    [SerializeField] private Transform cardParent;
    [SerializeField] private GameObject cardPrefab;

    [Inject] private DiContainer _container;
    
    private readonly List<MovableCard> _movableCards = new();
    
    private void Update()
    {
        foreach (var movableCard in _movableCards)
            movableCard.HandleUpdate(Time.deltaTime);
        
        if (Input.GetKeyDown(KeyCode.A))
            SpawnNewCard(null);
    }

    private void CheckFullLine()
    {
        if(_movableCards.Count < cardHolders.Length) return;
        
        bool allLineOccupied = true;
        foreach (var movableCard in _movableCards)
            if (!movableCard.IsReachDestination) allLineOccupied = false;
    
        if (allLineOccupied)
            Debug.Log("Game loosed");
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
    
    public void SpawnNewCard(CardConfigBase cardConfig)
    {           
        var cardProcessor = (CardProcessorBase)Activator.CreateInstance(cardConfig.CardProcessorBase.GetType());
        _container.Inject(cardProcessor);
        
        var movableCard = Instantiate(cardPrefab, cardParent).GetComponent<MovableCard>();
        movableCard.transform.position = cardSpawnPos.position;
        movableCard.SetCardData(cardProcessor, cardConfig.Sprite);
        movableCard.OnUse += RemoveCard;
        _movableCards.Add(movableCard);
        movableCard.OnReachDestination += CheckFullLine;

        for (int i = 0; i < cardHolders.Length; i++)
        {
            if (!cardHolders[i].HaveMovableCard)
            {
                cardHolders[i].SetCard();
                movableCard.SetDestination(i, cardHolders[i].transform);
                return;
            }
        }
        
        Debug.Log("Game loosed");
    }
}
