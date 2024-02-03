using UnityEngine;

namespace PathSystem
{
    [CreateAssetMenu(fileName = "PathConfig", menuName = "Configs/PathConfig")]
    public class PathConfig : ScriptableObject
    {
        [field: SerializeField] public float Lenght { get; private set; }
    }
}