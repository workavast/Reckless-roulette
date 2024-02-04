using UnityEngine;

namespace Configs.Cards.EffectCardConfig
{
    [CreateAssetMenu(fileName = nameof(PowerUpCardConfig), menuName = "Configs/Cards/" + nameof(PowerUpCardConfig))]
    public class PowerUpCardConfig : EffectCardConfigBase
    {
        [field: SerializeField] public float AdditionalDamage { get; private set; }
    }
}