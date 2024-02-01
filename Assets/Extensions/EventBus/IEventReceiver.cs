namespace EventBusExtension
{
    public interface IEventReceiver<in T> : IEventReceiverBase
        where T : struct, IEvent
    {
        public ReceiverIdentifier ReceiverIdentifier { get; }
        
        public void OnEvent(T t);
    }
}