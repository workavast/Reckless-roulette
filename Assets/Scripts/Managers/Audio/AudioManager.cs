using System;
using EventBusExtension;
using Events.Audio;
using UnityEngine;
using Zenject;

namespace Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : ManagerBase, IEventReceiver<CardUse>,IEventReceiver<SwordUse>,IEventReceiver<HealUse>
    {
        [Inject] private EventBus _eventBus;
        public ReceiverIdentifier ReceiverIdentifier { get; } = new();

        [SerializeField] private AudioClip cardUse;
        [SerializeField] private AudioClip swordUse;
        [SerializeField] private AudioClip healUse;

        private AudioSource _audioSource;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _eventBus.Subscribe(this as IEventReceiver<CardUse>);
            _eventBus.Subscribe(this as IEventReceiver<SwordUse>);
            _eventBus.Subscribe(this as IEventReceiver<HealUse>);
        }

        public void OnEvent(CardUse t) => _audioSource.PlayOneShot(cardUse);
        public void OnEvent(SwordUse t) => _audioSource.PlayOneShot(swordUse);
        public void OnEvent(HealUse t) => _audioSource.PlayOneShot(healUse);

        private void OnDestroy()
        {
            _eventBus.UnSubscribe(this as IEventReceiver<CardUse>);
            _eventBus.UnSubscribe(this as IEventReceiver<SwordUse>);
            _eventBus.UnSubscribe(this as IEventReceiver<HealUse>);
        }
    }
}