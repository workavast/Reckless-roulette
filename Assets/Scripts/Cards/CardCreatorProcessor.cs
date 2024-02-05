using System.Linq;
using UI_System.Elements;
using UnityEngine;

namespace Cards
{
    public class CardCreatorProcessor
    {
        private readonly CardType _bossCard;
        private readonly CardLine _cardLine;
        private readonly CardChanceRange[] _cardChances;
        
        public CardCreatorProcessor(LocationCardsConfig locationCardsConfig, CardLine cardLine)
        {
            _bossCard = locationCardsConfig.BossCard;
            _cardChances = locationCardsConfig.TakeCardChances().ToArray();
            _cardLine = cardLine;
        }
        
        public void CreateCard(CardType cardType) => _cardLine.SpawnNewCard(cardType);
        
        public void CreateRandomCard()
        {
            int chance = Random.Range(0, _cardChances[^1].UpCase);

            foreach (var cardChance in _cardChances)
            {
                if (cardChance.Check(chance))
                {
                    _cardLine.SpawnNewCard(cardChance.CardType);
                    return;
                }
            }
        }

        public void CreateBossCard()
        {
            _cardLine.SpawnNewCard(_bossCard);
        }
    }
}