using System;
using System.Collections;
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
            damageLevels.Init(this, _playerHero.DamageLevelSystem);
            healthPointsLevels.Init(this, _playerHero.HealthPointsLevelSystem);
            armorLevels.Init(this, _playerHero.ArmorLevelSystem);
        }
        
        [Serializable]
        private class PlayerLevelDater
        {
            [SerializeField] private TMP_Text level;
            [SerializeField] private Slider experienceBar;

            private PlayerHeroLevelVisualization _playerHeroLevelVisualization;
            private LevelSystemBaseBase _levelSystemBaseBase;
            private bool _coroutineIsActive;
            private float _targetValue;
            
            public void Init(PlayerHeroLevelVisualization playerHeroLevelVisualization, LevelSystemBaseBase levelSystemBaseBase)
            {
                _playerHeroLevelVisualization = playerHeroLevelVisualization;
                _levelSystemBaseBase = levelSystemBaseBase;
                _levelSystemBaseBase.ExperienceCounter.OnChange += UpdateInfo;
                _levelSystemBaseBase.LevelsCounter.OnChange += UpdateInfo;

                SetInfo();
            }

            private void SetInfo()
            {
                level.text = $"{_levelSystemBaseBase.LevelsCounter.CurrentValue}";
                experienceBar.value = _levelSystemBaseBase.ExperienceCounter.FillingPercentage;
            }
            
            private void UpdateInfo()
            {
                level.text = $"{_levelSystemBaseBase.LevelsCounter.CurrentValue}";
                // experienceBar.value = _levelSystemBaseBase.ExperienceCounter.FillingPercentage;
                
                _targetValue = _levelSystemBaseBase.ExperienceCounter.FillingPercentage;
                if (!_coroutineIsActive)
                    _playerHeroLevelVisualization.StartCoroutine(ChangeBarValue());
            }
            
            
            private IEnumerator ChangeBarValue()
            {
                _coroutineIsActive = true;

                while (Mathf.Abs(experienceBar.value - _targetValue) > 0.01f)
                {
                    experienceBar.value = Mathf.Lerp(experienceBar.value,_targetValue, 2.5f * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }

                experienceBar.value = _targetValue;
                _coroutineIsActive = false;
            }
        }
    }
}