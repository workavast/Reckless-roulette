using Configs.Cards;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CardsConfigsInstaller : MonoInstaller
    {
        [SerializeField] private CardConfigBase[] configs;

        public override void InstallBindings()
        {
            foreach (var config in configs)
            {
                Container.Bind(config.GetType()).FromInstance(config).AsSingle();
            }
        }
    }
}