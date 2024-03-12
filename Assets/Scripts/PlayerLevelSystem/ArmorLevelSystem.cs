using EventBusExtension;
using Events;

namespace PlayerLevelSystem
{
    public class ArmorLevelSystem : LevelSystemBase<ArmorLevelConfig, ArmorExp>
    {
        private readonly PlayerHero _playerHero;
        
        public ArmorLevelSystem(ArmorLevelConfig levelsConfig, EventBus eventBus, PlayerHero playerHero,
            int startLevel = 0, float startExp = 0) 
            : base(levelsConfig, eventBus, startLevel, startExp)
        {
            _playerHero = playerHero;
            _playerHero.ChangeArmor(LevelsConfig.Data[LevelsCounter.CurrentValue].Value);
        }

        protected override void ApplyLevelUp()
        {
            _playerHero.ChangeArmor(LevelsConfig.Data[LevelsCounter.CurrentValue].Value);
        }
    }
}