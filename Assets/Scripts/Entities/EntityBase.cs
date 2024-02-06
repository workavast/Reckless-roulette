using System;
using CustomTimer;
using EffectsSystem;
using SelectableSystem;
using SomeStorages;
using UnityEngine;

namespace Entities
{
    public abstract class EntityBase : MonoBehaviour, ISelectable
    {
        [SerializeField] protected SomeStorageFloat healthPoints;
        [SerializeField] protected float attackDamage;
        [Tooltip("attack speed per minute")] 
        [SerializeField] protected float attackSpeed;
        
        protected float FullAttackDamage => Mathf.Clamp(attackDamage + _additionalAttackDamage, 0, float.MaxValue);
        protected float FullTakeDamage => Mathf.Clamp(_additionalTakeDamage, 0, float.MaxValue);

        protected Timer AttackCooldown;
        protected bool IsDead;

        private float _additionalAttackDamage;
        private float _additionalTakeDamage;
        
        public EffectsProcessor EffectsProcessor;
        public IReadOnlySomeStorage<float> HealthPoints => healthPoints;
        
        protected event Action<float> OnUpdate;

        public abstract event Action OnTakeDamage;
        public abstract event Action OnAttack;
        
        private void Awake()
        {
            AttackCooldown = new Timer(60/attackSpeed);
            EffectsProcessor = new EffectsProcessor(this);

            OnUpdate += EffectsProcessor.HandleUpdate;
            OnUpdate += AttackCooldown.Tick;

            OnAwake();
        }
        
        protected virtual void OnAwake(){}
        
        public abstract void TakeDamage(float damage);
        
        public void ChangeAdditionalDamage(float additionalAttackDamage, float additionalTakeDamage)
        {
            _additionalAttackDamage += additionalAttackDamage;
            _additionalTakeDamage += additionalTakeDamage;
        }
        
        public void TakeHeal(float heal)
        {
            heal = Mathf.Clamp(heal, 0, float.MaxValue);
            healthPoints.ChangeCurrentValue(heal);
        }
        
        public void HandleUpdate(float time) => OnUpdate?.Invoke(time);
        
        //at the moment dont used
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