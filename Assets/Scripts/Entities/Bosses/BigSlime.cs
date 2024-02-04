using Entities.Enemies;
using Events;

namespace Entities.Bosses
{
    public class BigSlime : Enemy
    {
        protected override void OnAwake()
        {
            base.OnAwake();

            OnDie += CallDieGlobalEvent;
        }

        private void CallDieGlobalEvent(Enemy enemy) => EventBus.Invoke(new BossDie());
    }
}