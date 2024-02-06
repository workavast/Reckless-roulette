using System;
using System.Collections.Generic;
using Entities.Enemies;
using EventBusExtension;
using Events;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Entities.EnemiesGroups
{
    public class EnemyGroup : MonoBehaviour, IEventReceiver<PlayerHeroMove>
    {
        [SerializeField] private List<Enemy> enemies;
        
        public ReceiverIdentifier ReceiverIdentifier { get; } = new();
        public bool StayInFightPoint { get; private set; }
        
        private Transform _fightPoint;
        private readonly List<Enemy> _removeList = new();

        [Inject] protected EventBus EventBus;
        private bool _disposed;

        public event Action<EnemyGroup> OnGroupDie;

        protected void Awake()
        {
            EventBus.Subscribe(this);

            foreach (var enemy in enemies)
            {
                enemy.OnDie += RemoveEnemy;
                enemy.OnAttack += InitAttack;
            }
            
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            
        }

        private int _currentAttacker;
        private void InitAttack()
        {
            _currentAttacker++;
            if (_currentAttacker >= enemies.Count)
                _currentAttacker = 0;
            
            enemies[_currentAttacker].StartAttackCooldown();
        }
        
        public void HandleUpdate(float time)
        {
            foreach (var enemy in enemies)
                enemy.HandleUpdate(time);

            ClearRemoveList();
        }

        public void SetEnemiesCount(int enemiesCount)
        {
            enemiesCount = Mathf.Clamp(enemiesCount, 1, enemies.Count);

            for (int i = 0; i < enemies.Count - enemiesCount; i++)
                RemoveEnemy(enemies[i]);
            
            ClearRemoveList();
            
            InitAttack();
        }
        
        private void Move(float distance)
        {
            var dir = (_fightPoint.position - transform.position).normalized;
            
            transform.Translate(dir * distance);

            if (transform.position.x <= _fightPoint.position.x)
            {
                StayInFightPoint = true;

                foreach (var enemy in enemies)
                    enemy.ArriveFightPoint();
                
                EventBus.Invoke(new EnemyGroupReachFightPoint(this));
            }
        }
        
        private void RemoveEnemy(Enemy enemy)
        {
            _removeList.Add(enemy);
        }

        private void ClearRemoveList()
        {
            foreach (var enemy in _removeList)
            {
                enemies.Remove(enemy);
                Destroy(enemy.gameObject);
            }
            
            _removeList.Clear();
            
            if (enemies.Count <= 0)
                OnGroupDie?.Invoke(this);
        }
        
        public void SetPoints(Vector3 startPoint, Transform fightPoint)
        {
            transform.position = startPoint;
            _fightPoint = fightPoint;
        }
        
        public void TakeDamage(float damage)
        {
            var index = Random.Range(0, enemies.Count);
            enemies[index].TakeDamage(damage);
        }
        
        public void OnEvent(PlayerHeroMove @event) => Move(@event.MoveDistance);
        
        private void OnDestroy() => Dispose();

        private void Dispose()
        {
            if(_disposed) return;
            _disposed = true;
            EventBus.UnSubscribe(this);
        }
    }
}