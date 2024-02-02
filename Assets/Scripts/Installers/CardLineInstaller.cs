using UnityEngine;
using Zenject;

namespace Installers
{
    public class CardLineInstaller : MonoInstaller
    {
        [SerializeField] private CardLine cardLine;
        
        public override void InstallBindings()
        {
            Container.Bind<CardLine>().FromInstance(cardLine).AsSingle();
        }
    }
}