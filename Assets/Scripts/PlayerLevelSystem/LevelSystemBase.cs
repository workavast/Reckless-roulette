using EventBusExtension;
using Events;
using SomeStorages;

namespace PlayerLevelSystem
{
    public abstract class LevelSystemBase<TLevelsConfig, TEvent> : LevelSystemBaseBase, IEventReceiver<TEvent>
        where TLevelsConfig : LevelSystemConfigBase
        where TEvent : struct, IExpEvent
    {
        public ReceiverIdentifier ReceiverIdentifier { get; } = new();
        protected readonly TLevelsConfig LevelsConfig;
        private readonly EventBus _eventBus;
        
        protected LevelSystemBase(TLevelsConfig levelsConfig, EventBus eventBus)
        {
            _eventBus = eventBus;
            LevelsConfig = levelsConfig;
            _levelsCounter = new SomeStorageInt(LevelsConfig.Data.Count-1);
            _experience = new SomeStorageFloat(LevelsConfig.Data[_levelsCounter.CurrentValue].ExperienceCount);
            
            eventBus.Subscribe(this);
            _experience.OnCurrentValueChange += LevelUp;
        }

        public void OnEvent(TEvent t)
        {
            TakeExp(t.ExpValue);
        }
        
        private void TakeExp(float exp)
        {
            if(_levelsCounter.IsFull) return;
            
            _experience.ChangeCurrentValue(exp);
        }
        
        private void LevelUp()
        {
            if(!_experience.IsFull || _levelsCounter.IsFull)
                return;

            _levelsCounter.ChangeCurrentValue(1);
            _experience.SetMaxValue(LevelsConfig.Data[_levelsCounter.CurrentValue].ExperienceCount);
            _experience.SetCurrentValue(0);
            ApplyLevelUp();
            
            if(_levelsCounter.IsFull)
                _experience.OnCurrentValueChange -= LevelUp;
        }

        protected abstract void ApplyLevelUp();
    }
}