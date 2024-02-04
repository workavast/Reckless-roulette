using Enemies;
using Entities.Enemies;

namespace Factories
{
    public class EnemiesFactory : FactoryBase<EnemyType, Enemy>
    {
        protected override bool UseParents { get; } = true;
    }
}