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
        
        public override void UseCard()
        {
            _enemiesFactory.SpawnEnemy(EnemyType);
        }
    }
}