using Cards.Configs;

namespace EffectsSystem
{
    public class TimeHealEffect : EffectBase
    {
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