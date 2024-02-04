using Cards.CardsLogics.BossCards;
using Entities.Enemies;

namespace Factories
{
    public class BossFactory : FactoryBase<BossType, Enemy>
    {
        protected override bool UseParents => true;
    }
}