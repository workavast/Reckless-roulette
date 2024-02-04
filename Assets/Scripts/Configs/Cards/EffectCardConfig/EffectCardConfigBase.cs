using UnityEngine;

namespace Configs.Cards.EffectCardConfig
{
    public class EffectCardConfigBase : CardConfigBase
    {
        [field: SerializeField] public float TimeExist { get; protected set; }
        [field: SerializeField] public int AppliesCount { get; protected set; }
    }
}