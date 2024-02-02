using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "BlueSlimeCardConfig", menuName = "Cards/BlueSlimeCardConfig")]
    public class BlueSlimeCardConfig : CardConfigBase
    {
        public override CardProcessorBase CardProcessorBase { get; } = new BlueSlimeCard();
    }
}