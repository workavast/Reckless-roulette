using System.Linq;
using EnumValuesExtension;
using UnityEngine;

namespace Cards
{
    public class CardCreatorProcessor
    {
        private readonly CardsRepository _cardsRepository;
        private readonly CardLine _cardLine;

        private CardType[] _cardTypes;
        
        public CardCreatorProcessor(CardsRepository cardsRepository, CardLine cardLine)
        {
            _cardTypes = EnumValuesTool.GetValues<CardType>().ToArray();
            
            _cardsRepository = cardsRepository;
            _cardLine = cardLine;
        }
        
        public void CreateCard(CardType cardType) => _cardLine.SpawnNewCard(_cardsRepository.TakeCard(cardType));
        
        public void CreateRandomCard()
        {
            int index = Random.Range(0, _cardsRepository.CardsCount);
            
            _cardLine.SpawnNewCard(_cardsRepository.TakeCard(_cardTypes[index]));
        }
    }
}