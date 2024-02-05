using Managers;
using Zenject;

namespace Cards.CardsLogics.BossCards
{
    public abstract class BossCardLogicBase : CardLogicBase
    {
        protected abstract BossType BossType { get; }
        [Inject] private EnemiesManager _enemiesManager;

        public override bool TryUse(ICardTarget target)
        {
            _enemiesManager.SpawnBossGroup(BossType);

            return true;
        }
    }
}