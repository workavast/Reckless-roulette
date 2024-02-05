using UnityEngine;

namespace Configs.Cards.EnemyCardsConfigs
{
    public class EnemyCardConfigBase : CardConfigBase
    {
        [field: SerializeField] [field: Range(1, 3)] public int EnemiesCount { get; private set; } = 1;
    }
}