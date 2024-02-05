using EventBusExtension;
using Events;
using SomeStorages;

namespace PlayerLevelSystem
{
    public class HealthPointsLevelSystem : LevelSystemBase<HealthPointsLevelConfig, HealthPointsExp>
    {
        private readonly SomeStorageFloat _playerHealthPoints;

        public HealthPointsLevelSystem(HealthPointsLevelConfig healthPointsLevelConfig, EventBus eventBus, SomeStorageFloat playerHealthPoints) 
            : base(healthPointsLevelConfig, eventBus)
        {
            _playerHealthPoints = playerHealthPoints;
        }

        protected override void ApplyLevelUp()
        {
            _playerHealthPoints.SetMaxValue(_playerHealthPoints.MaxValue + LevelsConfig.Data[LevelsCounter.CurrentValue].Value);
        }
    }
}