using System;
using System.Collections.Generic;
using Entities;

namespace EffectsSystem
{
    public class EffectsProcessor
    {
        private readonly EntityBase _entity;

        private readonly List<EffectBase> _effects = new();
        private readonly List<EffectBase> _effectsForRemove = new();

        public event Action<EffectType> OnEffectsChange;
        
        public EffectsProcessor(EntityBase entity)
        {
            _entity = entity;
        }
        
        public void HandleUpdate(float time)
        {
            foreach (var effect in _effects)
                effect.HandleUpdate(time);

            RemoveEffects();
        }
        
        public void AddNewEffect(EffectBase effect)
        {
            effect.SetEntity(_entity);
            effect.OnEffectEnd += RemoveEffect;
            
            _effects.Add(effect);
            
            OnEffectsChange?.Invoke(effect.EffectType);
        }

        private void RemoveEffect(EffectBase effect)
        {
            effect.SetPause();
            _effectsForRemove.Add(effect);
        }
        
        private void RemoveEffects()
        {
            if(_effectsForRemove.Count <= 0) 
                return;

            foreach (var effect in _effectsForRemove)
            {
                _effects.Remove(effect);
                OnEffectsChange?.Invoke(effect.EffectType);
            }
            
            _effectsForRemove.Clear();
        }

        public int TakeEffectCount(EffectType effectType)
        {
            int effectCount = 0;
            
            foreach (var effect in _effects)
                if (effect.EffectType == effectType) effectCount++;

            return effectCount;
        }
    }
}