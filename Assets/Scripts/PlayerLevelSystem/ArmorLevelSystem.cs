using EventBusExtension;
using Events;

namespace PlayerLevelSystem
{
    public class ArmorLevelSystem : LevelSystemBase<ArmorLevelConfig, ArmorExp>
    {
        private readonly PlayerHero _playerHero;
        
        public ArmorLevelSystem(ArmorLevelConfig levelsConfig, EventBus eventBus, PlayerHero playerHero) : base(levelsConfig, eventBus)
        {
            _playerHero = playerHero;
        }

        protected override void ApplyLevelUp()
        {
            _playerHero.ChangeArmor(LevelsConfig.Data[LevelsCounter.CurrentValue].Value);
        }
    }
}