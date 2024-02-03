using System;
using CustomTimer;
using EventBusExtension;
using Events;
using SelectableSystem;
using SomeStorages;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IEventReceiver<PlayerHeroMove>, IHaveUI
    {
        [SerializeField] private SomeStorageFloat healthPoints;
        [SerializeField] private float attackDamage;
        [Tooltip("attack speed per minute")] 
        [SerializeField] private float attackSpeed;
        
        [Inject] private PlayerHero _playerHero;
        [Inject] private EventBus _eventBus;

        public ReceiverIdentifier ReceiverIdentifier { get; } = new();
        public IReadOnlySomeStorage<float> HealthPoints => healthPoints;
        public bool StayInFightPoint { get; private set; }
        
        private Transform _fightPoint;
        
        private Timer _attackCooldown;
        
        public event Action<Enemy> OnDie;
        public event Action<Enemy> OnReachFightPoint;
        public event Action<float> OnUpdate;

        private void Awake()
        {
            // _playerHero.OnMove += Move;
            _eventBus.Subscribe(this);

            _attackCooldown = new Timer(60 / attackSpeed);
                
            OnUpdate += _attackCooldown.Tick;
        }

        public void HandelUpdate(float time) => OnUpdate?.Invoke(time);

        public void SetFightPoint(Transform fightPoint)
        {
            _fightPoint = fightPoint;
        }
        
        public void TakeDamage(float damage)
        {
            healthPoints.ChangeCurrentValue(-damage);

            if (healthPoints.IsEmpty)
            {
                Destroy(gameObject);
                OnDie?.Invoke(this);
                Dispose();
            }
        }
        
        public void OnEvent(PlayerHeroMove @event)
        {
            Move(@event.MoveDistance);
        }

        private void Move(float distance)
        {
            transform.Translate(Vector3.left * distance);

            if (transform.position.x <= _fightPoint.position.x)
            {
                StayInFightPoint = true;
                OnReachFightPoint?.Invoke(this);

                _attackCooldown.OnTimerEnd += Attack;
                if(_attackCooldown.TimerIsEnd) Attack();
                
                _eventBus.Invoke(new EnemyReachFightPoint(this));
            }
        }

        private void Attack()
        {
            _attackCooldown.Reset();
            _playerHero.TakeDamage(attackDamage);
        }

        private void Dispose()
        {
            _eventBus.UnSubscribe(this);
        }

        public bool IsSelected { get; }
        public event Action OnSelect;
        public event Action OnDeselect;
        public void Select()
        {
            throw new NotImplementedException();
        }

        public void Deselect()
        {
            throw new NotImplementedException();
        }
    }
}