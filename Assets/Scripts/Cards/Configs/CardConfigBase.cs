using UnityEngine;

namespace Cards
{
    public abstract class CardConfigBase : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }

        public abstract CardProcessorBase CardProcessorBase { get; }
    }
}