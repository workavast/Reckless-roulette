using CastExtension;
using Configs.Cards.EffectCardConfig;
using EffectsSystem;
using Entities;
using Zenject;

namespace Cards.CardsLogics.EffectCards
{
    public class PowerUpCardLogic : CardLogicBase
    {
        [Inject] private PowerUpCardConfig _powerUpCardConfig;

        public override bool TryUse(ICardTarget target)
        {
            if (target.TryCast(out EntityBase entity))
            {
                var effect = new PowerUpEffect(_powerUpCardConfig);
                entity.EffectsProcessor.AddNewEffect(effect);
                return true;
            }

            return false;
        }
    }
}