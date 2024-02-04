using Configs.Cards.EffectCardConfig;

namespace EffectsSystem
{
    public class PoisonEffect : EffectBase
    {
        public override EffectType EffectType => EffectType.Poison;
        private readonly float _damage;

        public PoisonEffect(PoisonCardConfig poisonCardConfig) : base(poisonCardConfig)
        {
            _damage = poisonCardConfig.Damage;
        }
        
        protected override void EffectApply()
        {
            Entity.TakeDamage(_damage);
            ApplyCounter.ChangeCurrentValue(1);
            ApplyCooldown.Reset();
        }
    }
}