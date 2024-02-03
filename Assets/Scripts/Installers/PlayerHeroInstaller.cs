using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerHeroInstaller : MonoInstaller
    {
        [SerializeField] private PlayerHero playerHero;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerHero>().FromInstance(playerHero).AsSingle();
        }
    }
}