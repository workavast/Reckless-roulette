using Entities.Enemies;
using Events;

namespace Entities.Bosses
{
    public class BossBase : Enemy
    {
        protected override void OnAwake()
        {
            base.OnAwake();

            OnDie += CallDieGlobalEvent;
        }

        private void CallDieGlobalEvent(Enemy enemy) => EventBus.Invoke(new BossDie());
    }
}