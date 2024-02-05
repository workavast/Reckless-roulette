using Entities.Enemies;
using EventBusExtension;
using Events;
using Zenject;

namespace Entities.Bosses
{
    public class BigSlime : Enemy
    {
        [Inject] private EventBus _eventBus;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            OnDie += CallDieGlobalEvent;
        }

        private void CallDieGlobalEvent(Enemy enemy) => _eventBus.Invoke(new BossDie());
    }
}