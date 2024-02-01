using System;

namespace PoolSystem
{
    public interface IPoolable<TElement>
    {
        public event Action<TElement> OnDestroyElementEvent;
    
        public void OnExtractFromPool();
        public void OnReturnInPool();
    }

    public interface IPoolable<TElement, TId> : IPoolable<TElement>
    {
        public TId PoolId { get;}
    }
}