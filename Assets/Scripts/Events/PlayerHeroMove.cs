using EventBusExtension;

namespace Events
{
    public struct PlayerHeroMove : IEvent
    {
        public float MoveDistance { get; }

        public PlayerHeroMove(float moveDistance) => MoveDistance = moveDistance;
    }
}