using CastExtension;
using Configs.Cards;
using Entities;
using Zenject;

namespace Cards.CardsLogics
{
    public class HealCardLogic : CardLogicBase
    {
        [Inject] private HealCardConfig _healCardConfig; 

        public override bool TryUse(ICardTarget target)
        {
            if (target.TryCast(out EntityBase entity))
            {
                entity.TakeHeal(_healCardConfig.Heal);
                return true;
            }

            return false;
        }
    }
}