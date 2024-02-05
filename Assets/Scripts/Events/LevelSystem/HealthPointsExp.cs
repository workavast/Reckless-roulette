namespace Events
{
    public struct HealthPointsExp : IExpEvent
    {
        public float ExpValue { get; }

        public HealthPointsExp(float expValue)
        {
            ExpValue = expValue;
        }
    }
}