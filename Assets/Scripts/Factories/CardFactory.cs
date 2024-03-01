using Cards;
using UI_System.CardUi;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class CardFactory : FactoryInstallBase
    {
        [SerializeField] private DictionaryInspector<CardType, MovableCard> prefabs;
        protected DictionaryInspector<CardType, MovableCard> Prefabs = new();
        
        [Inject] private DiContainer _container;
        
        private void Awake()
        {
            Prefabs = prefabs;
        }

        public MovableCard Create(CardType type, Transform parent)
        {
            var newGameObject = _container.InstantiatePrefab(Prefabs[type], parent);
            
            return newGameObject.GetComponent<MovableCard>();
        }
    }
}