using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace Configs.Cards
{
    [CreateAssetMenu(fileName = nameof(ReshuffleCardConfig), menuName = "Configs/Cards/" + nameof(ReshuffleCardConfig))]
    public class ReshuffleCardConfig : CardConfigBase
    {
        [SerializeField] private List<CardType> possibleEnemy;
        [SerializeField] private List<CardType> possibleHeals;
        [field: SerializeField] [field: Range(0, 5)] public int HealthCardsCount { get; private set; }

        public IReadOnlyList<CardType> PossibleEnemy => possibleEnemy;
        public IReadOnlyList<CardType> PossibleHeals => possibleHeals;
    }
}