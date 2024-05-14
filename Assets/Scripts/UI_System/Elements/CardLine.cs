using System;
using System.Collections.Generic;
using Cards;
using Cards.CardsLogics.BossCards;
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
        [SerializeField] private float cardMoveSpeed;

        [Inject] private CardFactory _cardFactory;
        [Inject] private DiContainer _container;
        [Inject] private IGameCycleController _gameCycleController;
    
        private readonly List<MovableCard> _movableCards = new();

        private MovableCard _bossBuffer;
        
        public bool IsFull { get; private set; }
        public event Action OnFillLine;
        public event Action OnRemoveCard;

        public event Action<MovableCard> OnCardSpawn;
        
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
        
        private void BossBufferCheck()
        {
            if(_bossBuffer is null) return;
            
            for (int i = 0; i < cardHolders.Length; i++)
            {
                if (!cardHolders[i].HaveMovableCard)
                {
                    cardHolders[i].SetCard();
                    _bossBuffer.SetDestination(i, cardHolders[i].transform);
                    _movableCards.Add(_bossBuffer);
                    _bossBuffer = null;
                    return;
                }
            }
        }

        public void SpawnBossCard(CardType cardType)
        {
            var movableCard = _cardFactory.Create(cardType, cardParent);
            _container.Inject(movableCard);
            //need cus without this card spawned in the center of screen for one frame
            movableCard.transform.position = cardSpawnPos.position;
            movableCard.Init(cardSpawnPos, cardMoveSpeed);
            movableCard.OnUse += RemoveCard;
            movableCard.OnReachDestination += CheckFillLine;
            OnCardSpawn?.Invoke(movableCard);

            if (IsFull)
            {
                _bossBuffer = movableCard;
                OnRemoveCard += BossBufferCheck;
                return;
            }
            
            _movableCards.Add(movableCard);
            for (int i = 0; i < cardHolders.Length; i++)
            {
                if (!cardHolders[i].HaveMovableCard)
                {
                    cardHolders[i].SetCard();
                    movableCard.SetDestination(i, cardHolders[i].transform);
                    return;
                }
            }
            
            CheckFillLine();
        }

        
        public void SpawnNewCard(CardType cardType)
        {           
            if (IsFull) return;
            
            var movableCard = _cardFactory.Create(cardType, cardParent);
            _container.Inject(movableCard);
            //need cus without this card spawned in the center of screen for one frame
            movableCard.transform.position = cardSpawnPos.position;
            movableCard.Init(cardSpawnPos, cardMoveSpeed);
            movableCard.OnUse += RemoveCard;
            _movableCards.Add(movableCard);
            movableCard.OnReachDestination += CheckFillLine;
            OnCardSpawn?.Invoke(movableCard);

            for (int i = 0; i < cardHolders.Length; i++)
            {
                if (!cardHolders[i].HaveMovableCard)
                {
                    cardHolders[i].SetCard();
                    movableCard.SetDestination(i, cardHolders[i].transform);
                    return;
                }
            }

            CheckFillLine();
        }
        
        public void ClearCardLine()
        {
            List<MovableCard> cards = new(_movableCards); 
            foreach (var movableCard in cards)
            {
                if (!InheritsFrom(movableCard.CardLogicType, typeof(BossCardLogicBase)))
                    RemoveCard(movableCard.HolderIndex);
            }
        }
        
        private void CheckFillLine()
        {
            if (_movableCards.Count < cardHolders.Length)
            {
                IsFull = false;
                return;
            }

            if (!IsFull)
            {
                IsFull = true;
                OnFillLine?.Invoke();
            }
            
            IsFull = true;
        }

        private void RemoveCard(int holderIndex)
        {
            if(_movableCards.Count <= holderIndex) return;

            var movableCard = _movableCards[holderIndex];
            _movableCards.RemoveAt(holderIndex);
            cardHolders[holderIndex].ResetCard();
            Destroy(movableCard.gameObject);
            
            UpdateCardsDestinations(holderIndex);

            CheckFillLine();
            OnRemoveCard?.Invoke();
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
            _gameCycleController?.RemoveListener(GameCycleState.Gameplay, this as IGameCycleUpdate);
            _gameCycleController?.AddListener(GameCycleState.Gameplay, this as IGameCycleEnter);
            _gameCycleController?.AddListener(GameCycleState.Gameplay, this as IGameCycleExit);
        }
        
        private static bool InheritsFrom(Type type, Type baseType)
        {
            // check all base types
            var currentType = type;
            while (currentType != null)
            {
                if (currentType.BaseType == baseType)
                {
                    return true;
                }

                currentType = currentType.BaseType;
            }

            return false;
        }
    }
}