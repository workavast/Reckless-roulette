using System;
using System.Collections.Generic;
using Cards;
using Factories;
using GameCycle;
using UI_System.CardUi;
using UnityEngine;
using Zenject;

namespace UI_System.Elements
{
    public class CardLine : MonoBehaviour, IGameCycleUpdate, IGameCycleEnter, IGameCycleExit
    {
        [SerializeField] private CardHolder[] cardHolders;
        [SerializeField] private Transform cardSpawnPos;
        [SerializeField] private Transform cardParent;
        [SerializeField] private GameObject cardPrefab;

        [Inject] private CardFactory _cardFactory;
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
    
        public void SpawnNewCard(CardType cardType)
        {           
            var movableCard = _cardFactory.Create(cardType);
            _container.Inject(movableCard);
            //need cus without this card spawned in the center of screen for one frame
            movableCard.transform.position = cardSpawnPos.position;
            movableCard.transform.SetParent(transform);
            movableCard.SetStartPosition(cardSpawnPos);
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
}
