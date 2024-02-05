using EventBusExtension;
using Events;

namespace PlayerLevelSystem
{
    public class DamageLevelSystem : LevelSystemBase<DamageLevelConfig, DamageExp>
    {
        private readonly PlayerHero _playerHero;
        
        public DamageLevelSystem(DamageLevelConfig damageLevelConfig, EventBus eventBus, PlayerHero playerHero) 
            : base(damageLevelConfig, eventBus)
        {
            _playerHero = playerHero;
        }

        protected override void ApplyLevelUp()
        {
            _playerHero.ChangeAdditionalDamage(LevelsConfig.Data[LevelsCounter.CurrentValue].Value,0);
        }
    }
}