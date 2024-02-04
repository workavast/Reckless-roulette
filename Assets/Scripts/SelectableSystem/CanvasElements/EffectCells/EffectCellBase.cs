using EffectsSystem;
using TMPro;
using UnityEngine;

namespace SelectableSystem
{
    public abstract class EffectCellBase : MonoBehaviour
    {
        [SerializeField] private TMP_Text effectCount;
        
        public abstract EffectType EffectType { get; }
        
        public void SetNum(int newCount)
        {
            effectCount.text = $"{newCount}";
            
            gameObject.SetActive(newCount > 0);
        }
    }
}