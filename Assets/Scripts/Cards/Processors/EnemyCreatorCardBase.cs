using Enemies;
using Factories;
using Managers;
using Zenject;

namespace Cards
{
    public abstract class EnemyCreatorCardBase : CardProcessorBase
    {
        protected abstract EnemyType EnemyType { get; }
        [Inject] private EnemiesManager _enemiesFactory;
        
        public override bool TryUseCard(ICardTarget target)
        {
            _enemiesFactory.SpawnEnemy(EnemyType);

            return true;
        }
    }
}