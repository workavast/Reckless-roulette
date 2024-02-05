using Entities.Enemies;
using EventBusExtension;

namespace Events
{
    public struct EnemyReachFightPoint : IEvent
    {
        public Enemy Enemy { get; }

        public EnemyReachFightPoint(Enemy enemy) => Enemy = enemy;
    }
}