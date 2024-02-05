using CastExtension;
using Configs.Cards.EffectCardConfig;
using EffectsSystem;
using Entities;
using Zenject;

namespace Cards.CardsLogics.EffectCards
{
    public class RecklessRageCardLogic : CardLogicBase
    {
        [Inject] private RecklessRageCardConfig _config;
        
        public override bool TryUse(ICardTarget target)
        {
            if (target.TryCast(out EntityBase entity))
            {
                var effect = new RecklessRageEffect(_config);
                entity.EffectsProcessor.AddNewEffect(effect);
                return true;
            }

            return false;
        }
    }
}