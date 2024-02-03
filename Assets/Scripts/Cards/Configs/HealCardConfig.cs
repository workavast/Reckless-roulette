using UnityEngine;

namespace Cards.Configs
{
    [CreateAssetMenu(fileName = "HealCardConfig", menuName = "Cards/HealCardConfig")]
    public class HealCardConfig : CardConfigBase
    {
        public override CardProcessorBase CardProcessorBase { get; } = new HealCard();
    }
}