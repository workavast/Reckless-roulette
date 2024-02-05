namespace Events
{
    public struct DamageExp : IExpEvent
    {
        public float ExpValue { get; }
        
        public DamageExp(float expValue)
        {
            ExpValue = expValue;
        }
    }
}