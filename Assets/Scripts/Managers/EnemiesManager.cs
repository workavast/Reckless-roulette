using System.Collections.Generic;
using Cards.CardsLogics.BossCards;
using Enemies;
using Entities.EnemiesGroups;
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
        [SerializeField] [Range(0,20)] private int maxEnemyGroupsCount;

        [Inject] private BossGroupFactory _bossGroupFactory;
        [Inject] private EnemiesGroupsFactory _enemyGroupsFactory;
        [Inject] private IGameCycleController _gameCycleController;
        
        private readonly List<EnemyGroup> _activeEnemyGroups = new();
        private readonly List<EnemyGroup> _removeList = new();

        private void Awake()
        {
            _gameCycleController.AddListener(GameCycleState.Gameplay, this);
        }

        public void GameCycleUpdate()
        {
            foreach (var enemyGroup in _activeEnemyGroups)
                enemyGroup.HandleUpdate(Time.deltaTime);

            DestroyRemoveEnemyGroups();
        }
        
        public void SpawnBossGroup(BossType bossType)
        {
            foreach (var enemyGroup in _activeEnemyGroups)
                if(enemyGroup.transform.position.x >= spawnPoint.position.x)
                    RemoveEnemyGroup(enemyGroup);
            
            DestroyRemoveEnemyGroups();
        
            var bossGroup = _bossGroupFactory.Create(bossType);
            bossGroup.SetEnemiesCount(int.MaxValue);
            bossGroup.SetPoints(spawnPoint.position, fightPoint);
            bossGroup.OnGroupDie += RemoveEnemyGroup;
            _activeEnemyGroups.Add(bossGroup);
        }

        public bool TrySpawnEnemyGroup(EnemyType enemyType, int enemiesCount)
        {
            if (_activeEnemyGroups.Count >= maxEnemyGroupsCount) 
                return false;

            SpawnEnemyGroup(enemyType, enemiesCount);
            return true;
        }
        
        public void SpawnEnemyGroup(EnemyType enemyType, int enemiesCount)
        {
            var enemyGroup = _enemyGroupsFactory.Create(enemyType);
            
            float lastEnemyPos = 0;
            if(_activeEnemyGroups.Count > 0)
                lastEnemyPos = _activeEnemyGroups[^1].transform.position.x;
            
            float offset;
            if (lastEnemyPos + step <= spawnPoint.transform.position.x)
                offset = 0;
            else
                offset = lastEnemyPos + step - spawnPoint.transform.position.x;

            enemyGroup.SetEnemiesCount(enemiesCount);
            enemyGroup.SetPoints(spawnPoint.position + Vector3.right * offset, fightPoint);
            enemyGroup.OnGroupDie += RemoveEnemyGroup;
            _activeEnemyGroups.Add(enemyGroup);
        }
        
        private void RemoveEnemyGroup(EnemyGroup enemy)
        {
            _removeList.Add(enemy);
        }

        private void DestroyRemoveEnemyGroups()
        {
            foreach (var enemyGroup in _removeList)
            {
                _activeEnemyGroups.Remove(enemyGroup);
                Destroy(enemyGroup.gameObject);
            }
            
            _removeList.Clear();
        }

        private void OnDestroy()
        {
            _gameCycleController.RemoveListener(GameCycleState.Gameplay, this);
        }
    }
}