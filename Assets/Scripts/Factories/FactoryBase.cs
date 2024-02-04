using System;
using System.Collections.Generic;
using EnumValuesExtension;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Factories
{
    public abstract class FactoryBase<TEnum, TObject> : FactoryInstallBase
        where TEnum : Enum
        where TObject : Object
    {
        protected abstract bool UseParents { get; }
        
        [SerializeField] private DictionaryInspector<TEnum, TObject> prefabs;

        private readonly Dictionary<TEnum, Transform> _parents = new ();

        [Inject] private DiContainer _container;
        
        private void Awake()
        {
            var types = EnumValuesTool.GetValues<TEnum>();

            if(!UseParents) return;
            
            foreach (var type in types)
            {
                var parent = new GameObject
                {
                    name = type.ToString(),
                    transform = {parent = transform}
                };
                _parents.Add(type, parent.transform);
            }
        }

        public TObject Create(TEnum type)
        {
            var newGameObject = UseParents 
                ? _container.InstantiatePrefab(prefabs[type], _parents[type]) 
                : _container.InstantiatePrefab(prefabs[type]);
            
            return newGameObject.GetComponent<TObject>();
        }
    }
}