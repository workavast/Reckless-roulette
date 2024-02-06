using System;
using System.Collections.Generic;
using CustomTimer;
using UI_System.Elements;
using UnityEngine;
using Zenject;

namespace Cards
{
    public class CardCreatorWithPause : MonoBehaviour
    {
        [Inject] private CardLine _cardLine;
        
        private readonly List<CardType> _cardTypes = new();

        private Timer _timer;
        
        private void Awake()
        {
            _timer = new Timer(1);
            _timer.OnTimerEnd += SpawnCard;
        }

        public void AddCard(CardType cardType)
        {
            if(_cardTypes.Count <= 0 )
                _timer.Reset();
            
            _cardTypes.Add(cardType);
        }

        private void Update()
        {
            _timer.Tick(Time.deltaTime);
        }

        private void SpawnCard()
        {
            if(_cardTypes.Count <= 0) return;
            
            _cardLine.SpawnNewCard(_cardTypes[0]);
            
            _cardTypes.RemoveAt(0);
            _timer.Reset();
        }
    }
}