using System.Collections;
using SomeStorages;
using UnityEngine;
using UnityEngine.UI;

namespace SelectableSystem
{
    public interface ShowableUI
    {
        void Show();
        void Hide();
    }

    public class  HealthBar : MonoBehaviour, ShowableUI
    {
        [SerializeField] private Slider healthPointsSlider;
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
            healthPointsSlider.value = _healthStorage.FillingPercentage;
        }
        
        private void UpdateHealthBarValue()
        {
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
    }
}