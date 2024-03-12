using EventBusExtension;
using Events;
using SomeStorages;
using UnityEngine;

namespace PlayerLevelSystem
{
    public class HealthPointsLevelSystem : LevelSystemBase<HealthPointsLevelConfig, HealthPointsExp>
    {
        private readonly SomeStorageFloat _playerHealthPoints;

        public HealthPointsLevelSystem(HealthPointsLevelConfig healthPointsLevelConfig, EventBus eventBus,
            SomeStorageFloat playerHealthPoints, int startLevel = 0, float startExp = 0) 
            : base(healthPointsLevelConfig, eventBus, startLevel, startExp)
        {
            _playerHealthPoints = playerHealthPoints;
            
            var prevMaxValue = _playerHealthPoints.MaxValue;
            _playerHealthPoints.SetMaxValue(LevelsConfig.Data[LevelsCounter.CurrentValue].Value);
            _playerHealthPoints.ChangeCurrentValue(_playerHealthPoints.MaxValue - prevMaxValue);
        }

        protected override void ApplyLevelUp()
        {
            var prevMaxValue = _playerHealthPoints.MaxValue;
            _playerHealthPoints.SetMaxValue(LevelsConfig.Data[LevelsCounter.CurrentValue].Value);
            _playerHealthPoints.ChangeCurrentValue(_playerHealthPoints.MaxValue - prevMaxValue);
        }
    }
}