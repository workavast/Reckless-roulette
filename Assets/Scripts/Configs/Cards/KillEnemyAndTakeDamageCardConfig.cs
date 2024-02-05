using UnityEngine;

namespace Configs.Cards
{
    [CreateAssetMenu(fileName = nameof(KillEnemyAndTakeDamageCardConfig), menuName = "Configs/Cards/" + nameof(KillEnemyAndTakeDamageCardConfig))]
    public class KillEnemyAndTakeDamageCardConfig : CardConfigBase
    {
        [field: SerializeField] public float PlayerDamage { get; private set; }
    }
}