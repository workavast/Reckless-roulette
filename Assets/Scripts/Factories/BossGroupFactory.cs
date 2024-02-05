using Cards.CardsLogics.BossCards;
using Entities.EnemiesGroups;

namespace Factories
{
    public class BossGroupFactory : FactoryBase<BossType, EnemyGroup>
    {
        protected override bool UseParents => true;
    }
}