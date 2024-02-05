using UI_System.Elements;
using Zenject;

namespace Cards.CardsLogics
{
    public class ClearCardLineCardLogic : CardLogicBase
    {
        [Inject] private CardLine _cardLine;
        
        public override bool TryUse(ICardTarget target)
        {
            _cardLine.ClearCardLine();

            return false;
        }
    }
}