using System;
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
        
        public EffectsProcessor EffectsProcessor;
        public IReadOnlySomeStorage<float> HealthPoints => healthPoints;

        // protected SomeStorageFloat _currentAttackDamage;

        
        protected event Action<float> OnUpdate;
        
        private void Awake()
        {
            EffectsProcessor = new EffectsProcessor(this);

            OnUpdate += EffectsProcessor.HandleUpdate;

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