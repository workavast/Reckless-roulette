using System;
using Cards;
using EventBusExtension;
using Events;
using PlayerLevelSystem;
using UnityEngine;
using Zenject;

namespace Entities.Enemies
{
    [RequireComponent(typeof(Collider2D))]
    public class Enemy : EntityBase, ICardTarget
    {
        [SerializeField] private ExpType  expType;
        [SerializeField] private float expValue;
        
        [Inject] protected PlayerHero PlayerHero;
        [Inject] protected EventBus EventBus;

        public ReceiverIdentifier ReceiverIdentifier { get; } = new();
        public bool StayInFightPoint { get; private set; }

        private Transform _fightPoint;

        public event Action<Enemy> OnDie;
        
        public override event Action OnTakeDamage;
        public override event Action OnAttack;

        protected override void OnAwake()
        {
            base.OnAwake();
            
            AttackCooldown.SetPause();
            OnDie += DropExp;
        }

        public override void TakeDamage(float damage)
        {
            if (IsDead) return;

            healthPoints.ChangeCurrentValue(-(damage + FullTakeDamage));

            if (healthPoints.IsEmpty)
            {
                IsDead = true;
                OnDie?.Invoke(this);
            }
            
            OnTakeDamage?.Invoke();
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
            PlayerHero.TakeDamage(FullAttackDamage);
            
            OnAttack?.Invoke();
        }

        public void StartAttackCooldown()
        {
            AttackCooldown.Continue();
        }

        private void DropExp(Enemy enemy)
        {
            switch (expType)
            {
                case ExpType.Damage:
                    EventBus.Invoke(new DamageExp(expValue));
                    break;
                case ExpType.HealthPoints:
                    EventBus.Invoke(new HealthPointsExp(expValue));
                    break;
                case ExpType.Armor:
                    EventBus.Invoke(new ArmorExp(expValue));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}