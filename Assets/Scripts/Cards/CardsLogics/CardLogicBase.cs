using System;
using UnityEngine;

namespace Cards.CardsLogics
{
    public abstract class CardLogicBase : MonoBehaviour
    {
        public Type MyType => this.GetType();

        public abstract bool TryUse(ICardTarget target);
    }
}