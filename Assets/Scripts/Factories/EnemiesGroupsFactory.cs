using Enemies;
using Entities.EnemiesGroups;

namespace Factories
{
    public class EnemiesGroupsFactory : FactoryBase<EnemyType, EnemyGroup>
    {
        protected override bool UseParents => true;
    }
}