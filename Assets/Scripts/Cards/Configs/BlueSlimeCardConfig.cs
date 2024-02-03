using UnityEngine;

namespace Cards.Configs
{
    [CreateAssetMenu(fileName = "BlueSlimeCardConfig", menuName = "Cards/BlueSlimeCardConfig")]
    public class BlueSlimeCardConfig : CardConfigBase
    {
        public override CardProcessorBase CardProcessorBase { get; } = new BlueSlimeCard();
    }
}