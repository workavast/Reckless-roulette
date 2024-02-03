using GameCycle;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameCycleControllerInstaller : MonoInstaller
    {
        [SerializeField] private GameCycleController gameCycleController;
        
        public override void InstallBindings()
        {
            Container.Bind<GameCycleController>().FromInstance(gameCycleController).AsSingle();
            Container.Bind<IGameCycleController>().FromInstance(gameCycleController).AsSingle();
            Container.Bind<IGameCycleSwitcher>().FromInstance(gameCycleController).AsSingle();
        }
    }
}