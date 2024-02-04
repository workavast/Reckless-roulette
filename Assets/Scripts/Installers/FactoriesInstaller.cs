using Factories;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class FactoriesInstaller : MonoInstaller
    {
        [SerializeField] private FactoryInstallBase[] factories;
        
        public override void InstallBindings()
        {
            foreach (var factory in factories)
            {
                Container.Bind(factory.GetType()).FromInstance(factory).AsSingle();
            }
        }
    }
}