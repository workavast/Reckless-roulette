namespace Cards
{
    public class CardChanceRange
    {
        public CardType CardType { get; }
        public int LowCase { get; }
        public int UpCase { get; }

        public CardChanceRange(CardType cardType, int lowCase, int upCase)
        {
            CardType = cardType;
            LowCase = lowCase;
            UpCase = upCase;
        }
        
        /// <returns>
        ///     return true if value stay in [LowCase;UpCase)
        /// </returns>
        public bool Check(int value) => LowCase <= value && value < UpCase;
    }
}