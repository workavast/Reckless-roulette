using UnityEngine;

namespace Cards.Configs
{
    public class EffectCardConfigBase : CardConfigBase
    {
        [field: SerializeField] public float TimeExist { get; protected set; }
        [field: SerializeField] public int AppliesCount { get; protected set; }
    }
}