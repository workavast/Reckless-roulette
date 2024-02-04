using Managers;
using Zenject;

namespace Cards.CardsLogics.BossCards
{
    public abstract class BossCardLogicBase : CardLogicBase
    {
        protected abstract BossType BossType { get; }
        [Inject] private EnemiesManager _enemiesFactory;

        public override bool TryUse(ICardTarget target)
        {
            _enemiesFactory.SpawnBoss(BossType);

            return true;
        }
    }
}