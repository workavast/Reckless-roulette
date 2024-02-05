using Configs.Cards.EffectCardConfig;

namespace EffectsSystem
{
    public class PowerUpEffect : EffectBase
    {
        public override EffectType EffectType => EffectType.PowerUp;

        private readonly float _additionalDamage;
        
        public PowerUpEffect(PowerUpCardConfig powerUpCardConfig) : base(powerUpCardConfig)
        {
            _additionalDamage = powerUpCardConfig.AdditionalDamage;
            OnEntitySet += SetEffect;
            OnEffectEnd += RemoveEffect;
        }

        private void SetEffect()
        {
            Entity.ChangeAdditionalDamage(_additionalDamage, 0);
        }
        
        private void RemoveEffect(EffectBase effectBase)
        {
            Entity.ChangeAdditionalDamage(-_additionalDamage, 0);
        }
        
        protected override void EffectApply()
        {
            ApplyCounter.ChangeCurrentValue(1);
            ApplyCooldown.Reset();
        }
    }
}