using System.Collections.Generic;
using Enemies;
using Factories;
using GameCycle;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class EnemiesManager : ManagerBase, IGameCycleUpdate
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform fightPoint;
        [SerializeField] private float step;
        
        [Inject] private EnemiesFactory _enemiesFactory;
        [Inject] private IGameCycleController _gameCycleController;
        
        private readonly List<Enemy> _activeEnemies = new();

        private void Awake()
        {
            _gameCycleController.AddListener(GameCycleState.Gameplay, this);
        }

        public void GameCycleUpdate()
        {
            foreach (var enemy in _activeEnemies)
                enemy.HandelUpdate(Time.deltaTime);
        }
        
        public void SpawnEnemy(EnemyType enemyType)
        {
            var enemy = _enemiesFactory.SpawnEnemy(enemyType);
            enemy.SetFightPoint(fightPoint);

            float lastEnemyPos = 0;
            if(_activeEnemies.Count > 0)
                lastEnemyPos = _activeEnemies[^1].transform.position.x;

            float offset;
            if (lastEnemyPos + step <= spawnPoint.transform.position.x)
                offset = 0;
            else
                offset = lastEnemyPos + step - spawnPoint.transform.position.x;
            
            enemy.transform.position = spawnPoint.position + Vector3.right * offset;
            enemy.OnDie += RemoveEnemy;
            _activeEnemies.Add(enemy);
        }

        private void RemoveEnemy(Enemy enemy)
        {
            _activeEnemies.Remove(enemy);
        }

        private void OnDestroy()
        {
            _gameCycleController.RemoveListener(GameCycleState.Gameplay, this);
        }
    }
}