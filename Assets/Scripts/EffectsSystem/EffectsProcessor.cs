using System.Collections.Generic;
using Entities;

namespace EffectsSystem
{
    public class EffectsProcessor
    {
        private readonly EntityBase _entity;

        private readonly List<EffectBase> _effects = new();
        private readonly List<EffectBase> _effectsForRemove = new();

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

        public void Reset()
        {
            foreach (var effect in _effects)
                effect.SetPause();

            _effects.Clear();
        }
        
        public void AddNewEffect(EffectBase effect)
        {
            effect.SetEntity(_entity);
            effect.OnEffectEnd += RemoveEffect;
            
            _effects.Add(effect);
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
                _effects.Remove(effect);
            
            _effectsForRemove.Clear();
        }
    }
}