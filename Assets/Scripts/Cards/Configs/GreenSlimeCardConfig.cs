using UnityEngine;

namespace Cards.Configs
{
    [CreateAssetMenu(fileName = "GreenSlimeCardConfig", menuName = "Cards/GreenSlimeCardConfig")]
    public class GreenSlimeCardConfig : CardConfigBase
    {
        public override CardProcessorBase CardProcessorBase { get; } = new GreenSlimeCard();
    }
}