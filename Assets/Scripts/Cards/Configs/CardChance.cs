using System;
using UI_System.CardUi;
using UnityEngine;

namespace Cards.Configs
{
    [Serializable]
    public class CardChance
    {
        [field: SerializeField] public MovableCard MovableCard { get; private set; }
        [field: SerializeField] public int Chance { get; private set;}
    }
}