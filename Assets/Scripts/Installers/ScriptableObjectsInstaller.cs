using UnityEngine;
using Zenject;

namespace Installers
{
    public class ScriptableObjectsInstaller : MonoInstaller
    {
        [SerializeField] private ScriptableObject[] scriptableObjects;
        
        public override void InstallBindings()
        {
            foreach (var scriptableObject in scriptableObjects)
            {
                Container.Bind(scriptableObject.GetType()).FromInstance(scriptableObject).AsSingle();
            }
        }
    }
}