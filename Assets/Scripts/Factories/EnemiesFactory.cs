using System;
using System.Collections.Generic;
using Enemies;
using EnumValuesExtension;
using UnityEngine;

namespace Factories
{
    public class EnemiesFactory : MonoBehaviour
    {
        [SerializeField] private DictionaryInspector<EnemyType, GameObject> prefabs;

        private readonly Dictionary<EnemyType, Transform> _enemiesParents = new ();
        
        private void Awake()
        {
            var enemiesTypes = EnumValuesTool.GetValues<EnemyType>();

            foreach (var enemyType in enemiesTypes)
            {
                var enemyParent = new GameObject
                {
                    name = enemyType.ToString()
                };
                _enemiesParents.Add(enemyType, enemyParent.transform);
            }
        }

        public GameObject SpawnEnemy(EnemyType enemyType)
        {
            var enemy = Instantiate(prefabs[enemyType], _enemiesParents[enemyType]);
            return enemy;
        }
    }
}