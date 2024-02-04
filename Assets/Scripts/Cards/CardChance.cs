using System;
using UI_System.CardUi;
using UnityEngine;

namespace Cards
{
    [Serializable]
    public class CardChance
    {
        [field: SerializeField] public MovableCard MovableCard { get; private set; }
        [field: SerializeField] [field: Range(1, 100)] public int Chance { get; private set;}
    }
}