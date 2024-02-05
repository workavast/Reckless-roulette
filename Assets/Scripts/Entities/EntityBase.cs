using System;
using CustomTimer;
using EffectsSystem;
using SelectableSystem;
using SomeStorages;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities
{
    public abstract class EntityBase : MonoBehaviour, ISelectable
    {
        [SerializeField] protected SomeStorageFloat healthPoints;
        [SerializeField] protected float attackDamage;
        [Tooltip("attack speed per minute")] 
        [SerializeField] protected float attackSpeed;
        
        protected Timer AttackCooldown;
        
        public EffectsProcessor EffectsProcessor;
        public IReadOnlySomeStorage<float> HealthPoints => healthPoints;
        
        protected event Action<float> OnUpdate;
        
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

        public void ChangeDamage(float changeValue)
        {
            attackDamage += changeValue;
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