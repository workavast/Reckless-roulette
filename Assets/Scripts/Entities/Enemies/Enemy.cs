using System;
using Cards;
using CustomTimer;
using Enemies;
using EventBusExtension;
using Events;
using UnityEngine;
using Zenject;

namespace Entities.Enemies
{
    [RequireComponent(typeof(Collider2D))]
    public class Enemy : EntityBase, IEventReceiver<PlayerHeroMove>, ICardTarget
    {
        [field: SerializeField] public EnemyType PoolId { get; private set; }

        [Inject] private PlayerHero _playerHero;
        [Inject] private EventBus _eventBus;

        public ReceiverIdentifier ReceiverIdentifier { get; } = new();
        public bool StayInFightPoint { get; private set; }
        
        private Transform _fightPoint;
        private Timer _attackCooldown;

        public bool isDead;
        public event Action<Enemy> OnDie;

        protected override void OnAwake()
        {
            _eventBus.Subscribe(this);

            _attackCooldown = new Timer(60 / attackSpeed);
                
            OnUpdate += _attackCooldown.Tick;
        }
        
        public void SetFightPoint(Transform fightPoint)
        {
            _fightPoint = fightPoint;
        }
        
        public override void TakeDamage(float damage)
        {
            if(isDead) return;
            
            healthPoints.ChangeCurrentValue(-damage);

            if (healthPoints.IsEmpty)
            {
                isDead = true;
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
    }
}