using EventBusExtension;
using Zenject;

namespace Installers
{
    public class EventBusInstaller : MonoInstaller
    {
        private readonly EventBus _eventBus = new();
        
        public override void InstallBindings()
        {
            Container.Bind<EventBus>().FromInstance(_eventBus).AsSingle();
        }
    }
}