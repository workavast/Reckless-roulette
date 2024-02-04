using UnityEngine;

namespace Configs.Cards.EffectCardConfig
{
    [CreateAssetMenu(fileName = nameof(PoisonCardConfig), menuName = "Configs/Cards/" + nameof(PoisonCardConfig))]
    public class PoisonCardConfig : EffectCardConfigBase
    {
        [field: SerializeField] public float Damage { get; protected set; }
    }
}