using UnityEngine;

namespace Cards.Configs
{
    [CreateAssetMenu(fileName = "PoisonCardConfig", menuName = "Configs/Cards/PoisonCardConfig")]
    public class PoisonCardConfig : EffectCardConfigBase
    {
        [field: SerializeField] public float Damage { get; protected set; }
    }
}