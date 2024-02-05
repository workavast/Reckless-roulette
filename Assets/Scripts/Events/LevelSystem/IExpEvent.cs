using EventBusExtension;

namespace Events
{
    public interface IExpEvent : IEvent
    {
        public float ExpValue { get; }
    }
}