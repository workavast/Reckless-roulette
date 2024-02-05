using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerLevelSystem
{
    public class LevelSystemConfigBase : ScriptableObject
    {
        [SerializeField] private List<LevelData> data;
        public IReadOnlyList<LevelData> Data => data;
    }

    [Serializable]
    public class LevelData
    {
        [field: SerializeField] public int ExperienceCount { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
    }
}