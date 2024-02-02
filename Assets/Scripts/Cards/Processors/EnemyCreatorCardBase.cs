using Enemies;
using Factories;
using Zenject;

namespace Cards
{
    public abstract class EnemyCreatorCardBase : CardProcessorBase
    {
        protected abstract EnemyType EnemyType { get; }
        [Inject] private EnemiesFactory _enemiesFactory;
        
        public override void UseCard()
        {
            _enemiesFactory.SpawnEnemy(EnemyType);
        }
    }
}