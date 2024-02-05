using System;
using Cards;
using EventBusExtension;
using UnityEngine;
using Zenject;

namespace Entities.Enemies
{
    [RequireComponent(typeof(Collider2D))]
    public class Enemy : EntityBase, ICardTarget
    {
        [Inject] protected PlayerHero PlayerHero;

        public ReceiverIdentifier ReceiverIdentifier { get; } = new();
        public bool StayInFightPoint { get; private set; }

        private Transform _fightPoint;

        private bool _isDead;
        public event Action<Enemy> OnDie;
        public event Action OnAttack;

        protected override void OnAwake()
        {
            base.OnAwake();
            
            AttackCooldown.SetPause();
        }

        public override void TakeDamage(float damage)
        {
            if (_isDead) return;

            healthPoints.ChangeCurrentValue(-damage);

            if (healthPoints.IsEmpty)
            {
                _isDead = true;
                OnDie?.Invoke(this);
            }
        }

        public void ArriveFightPoint()
        { 
            StayInFightPoint = true;

            AttackCooldown.OnTimerEnd += Attack;
            if (AttackCooldown.TimerIsEnd) Attack();
        }

        private void Attack()
        {
            AttackCooldown.SetMaxValue(60/attackSpeed);
            AttackCooldown.Reset();
            AttackCooldown.SetPause();
            PlayerHero.TakeDamage(attackDamage);
            
            OnAttack?.Invoke();
        }

        public void StartAttackCooldown()
        {
            AttackCooldown.Continue();
        }
    }
}