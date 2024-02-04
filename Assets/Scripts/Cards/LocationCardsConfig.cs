using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = nameof(LocationCardsConfig), menuName = "Configs/Cards/" + nameof(LocationCardsConfig))]
    public class LocationCardsConfig : ScriptableObject
    {
        [SerializeField] private DictionaryInspector<CardType, CardChance> cards;

        public int CardsCount => cards.Count;
        
        public IReadOnlyList<CardChanceRange> TakeCardChances()
        {
            int fullNum = 0;
            List<CardChanceRange> cardsChanceRanges = new();
            
            foreach (var card in cards)
            {
                cardsChanceRanges.Add(new CardChanceRange(card.Key, fullNum, fullNum + card.Value.Chance));
                fullNum += card.Value.Chance;
            }
            
            return cardsChanceRanges;
        }

        public List<CardType> TakeCardTypes() => cards.Keys.ToList();
    }
}