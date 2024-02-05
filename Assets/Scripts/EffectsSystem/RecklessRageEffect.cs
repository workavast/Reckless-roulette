using Configs.Cards.EffectCardConfig;

namespace EffectsSystem
{
    public class RecklessRageEffect : EffectBase
    {
        public override EffectType EffectType => EffectType.RecklessRage;

        private readonly float _additionalAttackDamage;
        private readonly float _additionalTakeDamage;
        
        public RecklessRageEffect(RecklessRageCardConfig recklessRageCardConfig) : base(recklessRageCardConfig)
        {
            _additionalAttackDamage = recklessRageCardConfig.AdditionalAttackDamage;
            _additionalTakeDamage = recklessRageCardConfig.AdditionalTakeDamage;
            OnEntitySet += SetEffect;
            OnEffectEnd += RemoveEffect;
        }

        private void SetEffect()
        {
            Entity.ChangeAdditionalDamage(_additionalAttackDamage, _additionalTakeDamage);
        }
        
        private void RemoveEffect(EffectBase effectBase)
        {
            Entity.ChangeAdditionalDamage(-_additionalAttackDamage, -_additionalTakeDamage);
        }
        
        protected override void EffectApply()
        {
            ApplyCounter.ChangeCurrentValue(1);
            ApplyCooldown.Reset();
        }
    }
}