using Enemies;
using Managers;
using Zenject;

namespace Cards.CardsLogics
{
    public abstract class EnemyCreatorCardLogicBase : CardLogicBase
    {
        protected abstract EnemyType EnemyType { get; }
        [Inject] private EnemiesManager _enemiesFactory;

        public override bool TryUse(ICardTarget target)
        {
            _enemiesFactory.SpawnEnemy(EnemyType);

            return true;
        }
    }
}