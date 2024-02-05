using UnityEngine;

namespace Configs.Cards.EffectCardConfig
{
    [CreateAssetMenu(fileName = nameof(RecklessRageCardConfig), menuName = "Configs/Cards/" + nameof(RecklessRageCardConfig))]
    public class RecklessRageCardConfig : EffectCardConfigBase
    {
        [field: SerializeField] [field: Range(0,10)] public float AdditionalAttackDamage { get; protected set; }
        [field: SerializeField] [field: Range(0,10)] public float AdditionalTakeDamage { get; protected set; }
    }
}