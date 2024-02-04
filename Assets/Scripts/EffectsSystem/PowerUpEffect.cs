using Configs.Cards.EffectCardConfig;

namespace EffectsSystem
{
    public class PowerUpEffect : EffectBase
    {
        private readonly float _additionalDamage;
        
        public PowerUpEffect(PowerUpCardConfig powerUpCardConfig) : base(powerUpCardConfig)
        {
            _additionalDamage = powerUpCardConfig.AdditionalDamage;
            OnEntitySet += SetEffect;
            OnEffectEnd += RemoveEffect;
        }

        private void SetEffect()
        {
            Entity.ChangeDamage(_additionalDamage);
        }
        
        private void RemoveEffect(EffectBase effectBase)
        {
            Entity.ChangeDamage(-_additionalDamage);
        }
        
        protected override void EffectApply()
        {
            ApplyCounter.ChangeCurrentValue(1);
            ApplyCooldown.Reset();
        }
    }
}