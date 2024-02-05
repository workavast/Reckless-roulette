namespace Events
{
    public struct ArmorExp : IExpEvent
    {
        public float ExpValue { get; }

        public ArmorExp(float expValue)
        {
            ExpValue = expValue;
        }
    }
}