using UnityEngine;

namespace Configs.Cards
{
    [CreateAssetMenu(fileName = "HealCardConfig", menuName = "Configs/Cards/HealCardConfig")]
    public class HealCardConfig : CardConfigBase
    {
        [field: SerializeField] public float Heal { get; private set; }
    }
}