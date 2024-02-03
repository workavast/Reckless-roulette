using System;
using PathSystem;
using SomeStorages;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class PathManager : ManagerBase
    {
        [SerializeField] private PathConfig pathConfig;
        [Inject] private PlayerHero _playerHero;

        private SomeStorageFloat _lenght;
        public IReadOnlySomeStorage<float> Lenght => _lenght;
        
        public event Action OnPositionChange;
        public event Action OnArriveDestination;

        private void Awake()
        {
            _lenght = new SomeStorageFloat(pathConfig.Lenght);
            _playerHero.OnMove += UpdatePath;
        }

        public void ChangePathConfig(PathConfig newPathConfig)
        {
            pathConfig = newPathConfig;
            _lenght = new SomeStorageFloat(pathConfig.Lenght);
        }
        
        private void UpdatePath(float distance)
        {
            _lenght.ChangeCurrentValue(distance);
            OnPositionChange?.Invoke();
            
            if(_lenght.IsFull)
                OnArriveDestination?.Invoke();
        }
    }
}
