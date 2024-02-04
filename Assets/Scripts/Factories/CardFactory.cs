using Cards;
using UI_System.CardUi;

namespace Factories
{
    public class CardFactory : FactoryBase<CardType, MovableCard>
    {
        protected override bool UseParents { get; } = false;

        // [Inject] private CardsConfigsRepository _cardsConfigsRepository;
        // [Inject] private DiContainer _container;
        //
        // private readonly Dictionary<Type, CardLogicBase> _prefabs = new();
        // private readonly Dictionary<Type, Transform> _parents = new();
        //
        // private void Awake()
        // {
        //     foreach (var card in _cardsConfigsRepository.Cards)
        //     {
        //         var parent = new GameObject
        //         {
        //             name = card.GetType().ToString(),
        //             transform = {parent = transform}
        //         };
        //         _parents.Add(card.GetType(), parent.transform);
        //         _prefabs.Add(card.GetType(), card);
        //     }
        // }
        //
        // public CardLogicBase Create(Type cardType)
        // {
        //     if (!_prefabs.TryGetValue(cardType, out CardLogicBase card))
        //     {
        //         Debug.LogWarning("Error: invalid parameter in SetWindow(ScreenEnum screen)");
        //         return default;
        //     }
        //     
        //     var @object = _container.InstantiatePrefab(card, _parents[cardType]);
        //     return @object.GetComponent<CardLogicBase>();
        // }
        //
        // public CardLogicBase Create<TCardType>()
        //     where TCardType : CardLogicBase
        // {
        //     if (!_prefabs.TryGetValue(typeof(TCardType), out CardLogicBase card))
        //     {
        //         Debug.LogWarning("Error: invalid parameter in SetWindow(ScreenEnum screen)");
        //         return default;
        //     }
        //     
        //     var @object = _container.InstantiatePrefab(card, _parents[typeof(TCardType)]);
        //     return @object.GetComponent<CardLogicBase>();
        // }
    }
}