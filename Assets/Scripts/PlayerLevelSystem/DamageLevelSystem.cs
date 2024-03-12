using EventBusExtension;
using Events;

namespace PlayerLevelSystem
{
    public class DamageLevelSystem : LevelSystemBase<DamageLevelConfig, DamageExp>
    {
        private readonly PlayerHero _playerHero;
        
        public DamageLevelSystem(DamageLevelConfig damageLevelConfig, EventBus eventBus, PlayerHero playerHero, 
            int startLevel = 0, float startExp = 0) 
            : base(damageLevelConfig, eventBus, startLevel, startExp)
        {
            _playerHero = playerHero;
            _playerHero.ChangeDamage(LevelsConfig.Data[LevelsCounter.CurrentValue].Value);
        }

        protected override void ApplyLevelUp()
        {
            _playerHero.ChangeDamage(LevelsConfig.Data[LevelsCounter.CurrentValue].Value);
        }
    }
}