using Cards.Configs;
using UI_System.CardUi;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cards
{
    [CreateAssetMenu(fileName = "CardsRepository", menuName = "Configs/Cards/CardsRepository")]
    public class CardsRepository : ScriptableObject
    {
        [SerializeField] private DictionaryInspector<CardType, CardChance> cards;

        public int CardsCount => cards.Count;
        
        public MovableCard TakeCard(CardType cardType)
        {
            return cards[cardType].MovableCard;
        }
    }
}