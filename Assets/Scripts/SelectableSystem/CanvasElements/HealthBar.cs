using System;
using System.Collections;
using SomeStorages;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SelectableSystem
{
    public class  HealthBar : MonoBehaviour, IShowableUI
    {
        [SerializeField] private Slider healthPointsSlider;
        [SerializeField] private TMP_Text healthPointsValue;
        private IReadOnlySomeStorage<float> _healthStorage;

        private bool _coroutineIsActive;
        private float _targetValue;
        
        public void Init(IReadOnlySomeStorage<float> healthStorage)
        {
            _healthStorage = healthStorage;
            SetHealthBarValue();
        }

        public void Show()
        {
            _healthStorage.OnChange += UpdateHealthBarValue;
            SetHealthBarValue();
        }

        public void Hide()
        {
            _healthStorage.OnChange -= UpdateHealthBarValue;
            if (_coroutineIsActive)
            {
                StopCoroutine(ChangeHealthBarValue());
                _coroutineIsActive = false;
            }
        }
        
        private void SetHealthBarValue()
        {
            var curValue = RoundNumber(_healthStorage.CurrentValue);
            var maxValue = RoundNumber(_healthStorage.MaxValue);
            healthPointsValue.text = $"{curValue}/{maxValue}";         
            
            healthPointsSlider.value = _healthStorage.FillingPercentage;
        }
        
        private void UpdateHealthBarValue()
        {
            var curValue = RoundNumber(_healthStorage.CurrentValue);
            var maxValue = RoundNumber(_healthStorage.MaxValue);
            healthPointsValue.text = $"{curValue}/{maxValue}";         
            
            _targetValue = _healthStorage.FillingPercentage;
            if (!_coroutineIsActive)
                StartCoroutine(ChangeHealthBarValue());
        }

        private IEnumerator ChangeHealthBarValue()
        {
            _coroutineIsActive = true;

            while (Mathf.Abs(healthPointsSlider.value - _targetValue) > 0.01f)
            {
                healthPointsSlider.value = Mathf.Lerp(healthPointsSlider.value,_targetValue, 2.5f * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            healthPointsSlider.value = _targetValue;
            _coroutineIsActive = false;
        }

        private static float RoundNumber(float num)
        {
            var curValue = Math.Round((decimal)num, 1);

            if (curValue % 10 == 0)
                return (int)curValue;

            return (float)curValue;
        }
    }
}