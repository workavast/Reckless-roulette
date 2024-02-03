using System.Collections.Generic;
using Enemies;
using EnumValuesExtension;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class EnemiesFactory : MonoBehaviour
    {
        [SerializeField] private DictionaryInspector<EnemyType, Enemy> prefabs;

        private readonly Dictionary<EnemyType, Transform> _enemiesParents = new ();

        [Inject] private DiContainer _container;
        
        private void Awake()
        {
            var enemiesTypes = EnumValuesTool.GetValues<EnemyType>();

            foreach (var enemyType in enemiesTypes)
            {
                var enemyParent = new GameObject
                {
                    name = enemyType.ToString(),
                    transform = {parent = transform}
                };
                _enemiesParents.Add(enemyType, enemyParent.transform);
            }
        }

        public Enemy SpawnEnemy(EnemyType enemyType)
        {
            var enemy = _container.InstantiatePrefab(prefabs[enemyType], _enemiesParents[enemyType]);
            return enemy.GetComponent<Enemy>();
        }
    }
}