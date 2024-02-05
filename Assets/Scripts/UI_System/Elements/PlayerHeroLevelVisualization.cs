using System;
using PlayerLevelSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI_System.Elements
{
    public class PlayerHeroLevelVisualization : MonoBehaviour
    {
        [Inject] private PlayerHero _playerHero;

        [SerializeField] private PlayerLevelDater damageLevels;
        [SerializeField] private PlayerLevelDater healthPointsLevels;
        [SerializeField] private PlayerLevelDater armorLevels;
        
        private void Start()
        {
            damageLevels.Init(_playerHero.DamageLevelSystem);
            healthPointsLevels.Init(_playerHero.HealthPointsLevelSystem);
            armorLevels.Init(_playerHero.ArmorLevelSystem);
        }
        
        [Serializable]
        private class PlayerLevelDater
        {
            [SerializeField] private TMP_Text level;
            [SerializeField] private Slider experienceBar;

            private LevelSystemBaseBase _levelSystemBaseBase;
        
            public void Init(LevelSystemBaseBase levelSystemBaseBase)
            {
                _levelSystemBaseBase = levelSystemBaseBase;
                _levelSystemBaseBase.ExperienceCounter.OnChange += UpdateInfo;
                _levelSystemBaseBase.LevelsCounter.OnChange += UpdateInfo;
                UpdateInfo();
            }

            private void UpdateInfo()
            {
                level.text = $"{_levelSystemBaseBase.LevelsCounter.CurrentValue}";
                experienceBar.value = _levelSystemBaseBase.ExperienceCounter.FillingPercentage;
            }
        }
    }
}