using System;
using System.Threading.Tasks;

namespace TrainLevel
{
    public abstract class TrainBlockBase
    {
        public abstract event Action OnEnd;

        public abstract void Enter();
        
        protected async Task Wait(int n)
        {
            await Task.Delay(n);
        }
    }
}