using UnityEngine;

namespace Cards.Configs
{
    [CreateAssetMenu(fileName = "TimeHealEffectCardConfig", menuName = "Configs/Cards/TimeHealEffectCardConfig")]
    public class TimeHealEffectCardConfig : EffectCardConfigBase
    {
        [field: SerializeField] public float Heal { get; private set; }
    }
}