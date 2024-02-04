using System.Collections.Generic;
using EffectsSystem;
using UnityEngine;

namespace SelectableSystem
{
    public class EffectsBar : MonoBehaviour, IShowableUI
    {
        private readonly Dictionary<EffectType, EffectCellBase> _effectCells = new();

        private EffectsProcessor _effectsProcessor;

        private void Awake()
        {
            EffectCellBase[] effectCells = GetComponentsInChildren<EffectCellBase>();
            foreach (var effectCell in effectCells)
                _effectCells.Add(effectCell.EffectType, effectCell);
        }

        public void Init(EffectsProcessor effectsProcessor)
        {
            _effectsProcessor = effectsProcessor;
        }
        
        public void Show()
        {
            _effectsProcessor.OnEffectsChange += UpdateEffectsVisualization;

            foreach (var effect in _effectCells)
                UpdateEffectsVisualization(effect.Key);
        }

        public void Hide()
        {
            _effectsProcessor.OnEffectsChange -= UpdateEffectsVisualization;
        }
        
        private void UpdateEffectsVisualization(EffectType effectType) =>
            _effectCells[effectType].SetNum(_effectsProcessor.TakeEffectCount(effectType));
    }
}