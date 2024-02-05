using Entities.EnemiesGroups;
using EventBusExtension;

namespace Events
{
    public struct EnemyGroupReachFightPoint : IEvent
    {
        public EnemyGroup EnemyGroup { get; }

        public EnemyGroupReachFightPoint(EnemyGroup enemyGroup) => EnemyGroup = enemyGroup;
    }
}