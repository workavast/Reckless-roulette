namespace Cards
{
    public abstract class CardProcessorBase
    {
        public abstract bool TryUseCard(ICardTarget target);
    }
}