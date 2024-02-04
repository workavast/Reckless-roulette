using UnityEngine;

namespace Cards.CardsLogics
{
    public abstract class CardLogicBase : MonoBehaviour
    {
        public abstract bool TryUse(ICardTarget target);
    }
}