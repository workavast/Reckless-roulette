using CastExtension;
using Configs.Cards.EffectCardConfig;
using EffectsSystem;
using Entities;
using Zenject;

namespace Cards.CardsLogics.EffectCards
{
    public class PoisonCardLogic : CardLogicBase
    {
        [Inject] private PoisonCardConfig _poisonCardConfig;
        
        public override bool TryUse(ICardTarget target)
        {
            if (target.TryCast(out EntityBase entity))
            {
                var effect = new PoisonEffect(_poisonCardConfig);
                entity.EffectsProcessor.AddNewEffect(effect);
                return true;
            }

            return false;
        }
    }
}