using UnityEngine;

namespace Cards.Configs
{
    [CreateAssetMenu(fileName = "RedSlimeCardConfig", menuName = "Cards/RedSlimeCardConfig")]
    public class RedSlimeCardConfig : CardConfigBase
    {
        public override CardProcessorBase CardProcessorBase { get; } = new RedSlimeCard();
    }
}