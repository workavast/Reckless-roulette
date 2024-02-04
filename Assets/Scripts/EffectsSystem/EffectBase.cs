using System;
using Configs.Cards.EffectCardConfig;
using CustomTimer;
using Entities;
using SomeStorages;

namespace EffectsSystem
{
    public abstract class EffectBase
    {
        public abstract EffectType EffectType { get; }
        
        protected readonly Timer ApplyCooldown;
        protected readonly SomeStorageInt ApplyCounter;
        protected EntityBase Entity;
        
        public event Action<EffectBase> OnEffectEnd;
        public event Action OnEntitySet;
        
        protected EffectBase(EffectCardConfigBase effectCardConfigBase)
        {
            ApplyCooldown = new Timer(effectCardConfigBase.TimeExist/effectCardConfigBase.AppliesCount);
            ApplyCooldown.OnTimerEnd += EffectApply;

            ApplyCounter = new SomeStorageInt(effectCardConfigBase.AppliesCount);
            ApplyCounter.OnChange += CheckAppliesCount;
        }

        public void SetEntity(EntityBase entity)
        {
            Entity = entity;
            OnEntitySet?.Invoke();
        }

        public void HandleUpdate(float time) => ApplyCooldown.Tick(time);
        
        protected void CheckAppliesCount()
        {
            if (ApplyCounter.IsFull)
                OnEffectEnd?.Invoke(this);
        }
        
        protected abstract void EffectApply();

        public void SetPause()
        {
            ApplyCooldown.SetPause();
        }
    }
}