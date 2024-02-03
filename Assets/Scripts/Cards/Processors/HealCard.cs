using CastExtension;

namespace Cards
{
    public class HealCard : CardProcessorBase
    {
        private const float Health = 10;

        public override bool TryUseCard(ICardTarget target)
        {
            if (target.TryCast(out PlayerHero playerHero))
            {
                playerHero.TakeHeal(Health);
                return true;
            }

            return false;
        }
    }
}