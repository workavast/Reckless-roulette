using Configs.Cards.EffectCardConfig;

namespace EffectsSystem
{
    public class TimeHealEffect : EffectBase
    {
        public override EffectType EffectType => EffectType.TimeHeal;
        
        private readonly float _heal;

        public TimeHealEffect(TimeHealEffectCardConfig timeHealEffectCardConfig) : base(timeHealEffectCardConfig)
        {
            _heal = timeHealEffectCardConfig.Heal;
        }
        
        protected override void EffectApply()
        {
            Entity.TakeHeal(_heal);
            ApplyCounter.ChangeCurrentValue(1);
            ApplyCooldown.Reset();
        }
    }
}