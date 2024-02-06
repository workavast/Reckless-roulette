using CastExtension;
using Configs.Cards;
using Entities;
using EventBusExtension;
using Events.Audio;
using UnityEngine;
using Zenject;

namespace Cards.CardsLogics
{
    public class HealCardLogic : CardLogicBase
    {
        [SerializeField] private HealCardConfig _healCardConfig;
        [Inject] private EventBus _eventBus;
        
        public override bool TryUse(ICardTarget target)
        {
            if (target.TryCast(out EntityBase entity))
            {
                entity.TakeHeal(_healCardConfig.Heal);
                _eventBus.Invoke(new HealUse());
                return true;
            }

            return false;
        }
    }
}