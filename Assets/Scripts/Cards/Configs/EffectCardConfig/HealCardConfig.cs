using UnityEngine;

namespace Cards.Configs
{
    [CreateAssetMenu(fileName = "HealCardConfig", menuName = "Configs/Cards/HealCardConfig")]
    public class HealCardConfig : CardConfigBase
    {
        [field: SerializeField] public float Heal { get; private set; }
    }
}