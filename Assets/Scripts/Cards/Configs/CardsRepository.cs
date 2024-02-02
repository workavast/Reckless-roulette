using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "CardsRepository", menuName = "CardsRepository")]
    public class CardsRepository : ScriptableObject
    {
        [SerializeField] private DictionaryInspector<CardType, CardConfigBase> cards;

        public int CardsCount => cards.Count;
        
        public CardConfigBase TakeCard(CardType cardType) => cards[cardType];
    }
}