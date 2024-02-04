using System;
using Cards;
using CustomTimer;
using EventBusExtension;
using Events;
using UnityEngine;
using Zenject;

namespace Entities.Enemies
{
    [RequireComponent(typeof(Collider2D))]
    public class Enemy : EntityBase, IEventReceiver<PlayerHeroMove>, ICardTarget
    {
        [Inject] protected PlayerHero PlayerHero;
        [Inject] protected EventBus EventBus;

        public ReceiverIdentifier ReceiverIdentifier { get; } = new();
        public bool StayInFightPoint { get; private set; }
        
        private Transform _fightPoint;
        private Timer _attackCooldown;

        private bool _isDead;
        private bool _disposed;
        public event Action<Enemy> OnDie;

        protected override void OnAwake()
        {
            EventBus.Subscribe(this);

            _attackCooldown = new Timer(60 / attackSpeed);
                
            OnUpdate += _attackCooldown.Tick;
        }
        
        public void SetFightPoint(Transform fightPoint)
        {
            _fightPoint = fightPoint;
        }
        
        public override void TakeDamage(float damage)
        {
            if(_isDead) return;
            
            healthPoints.ChangeCurrentValue(-damage);

            if (healthPoints.IsEmpty)
            {
                _isDead = true;
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
                
                EventBus.Invoke(new EnemyReachFightPoint(this));
            }
        }

        private void Attack()
        {
            _attackCooldown.Reset();
            PlayerHero.TakeDamage(attackDamage);
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void Dispose()
        {
            if(_disposed) return;
            _disposed = true;
            EventBus.UnSubscribe(this);
        }
    }
}