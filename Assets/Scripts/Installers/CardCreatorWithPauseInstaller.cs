using Cards;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CardCreatorWithPauseInstaller : MonoInstaller
    {
        [SerializeField] private CardCreatorWithPause cardCreatorWithPause;
        
        public override void InstallBindings()
        {
            Container.Bind<CardCreatorWithPause>().FromInstance(cardCreatorWithPause).AsSingle();
        }
    }
}