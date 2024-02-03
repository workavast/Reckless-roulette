using Managers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI_System.CardUi
{
    public class PathVisualization : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        [Inject] private PathManager _pathManager;

        private void Awake()
        {
            _pathManager.OnPositionChange += UpdateSlider;
        }

        private void UpdateSlider()
        {
            slider.value = _pathManager.Lenght.FillingPercentage;
        }
    }
}