using Enemies;
using Managers;
using UnityEngine;
using Zenject;

namespace Cards.CardsLogics.EnemyCards
{
    public abstract class EnemyCreatorCardLogicBase : CardLogicBase
    {
        [field: SerializeField] [field: Range(1, 3)] public int EnemiesCount { get; private set; } = 1;
        
        protected abstract EnemyType EnemyType { get; }
        
        [Inject] private EnemiesManager _enemiesFactory;

        public override bool TryUse(ICardTarget target)
        {
            return  _enemiesFactory.TrySpawnEnemyGroup(EnemyType, EnemiesCount);
        }
    }
}