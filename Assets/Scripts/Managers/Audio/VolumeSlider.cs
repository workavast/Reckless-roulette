using System;
using Audio;
using Managers;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

namespace UI_System.Elements
{
    [RequireComponent(typeof(Slider))]
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private VolumeType volumeType;
        [SerializeField] private AudioMixer audioMixer;
        
        private AudioVolumeChanger _audioVolumeChanger;
        
        private Slider _slider;
        private event Action<float> OnValueChange;
        
        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(ChangeValue);

            _audioVolumeChanger = new AudioVolumeChanger(audioMixer);
        }

        private void Start()
        {
            _audioVolumeChanger.StartInit();
            switch (volumeType)
            {
                case VolumeType.Master:
                    _slider.value = _audioVolumeChanger.MasterVolume;
                    OnValueChange += SetMasterVolume;
                    break;
                case VolumeType.Ost:
                    _slider.value = _audioVolumeChanger.MusicVolume;
                    OnValueChange += SetOstVolume;
                    break;
                case VolumeType.Effects:
                    _slider.value = _audioVolumeChanger.EffectsVolume;
                    OnValueChange += SetEffectsVolume;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ChangeValue(float newValue) => OnValueChange?.Invoke(newValue);
        
        private void SetMasterVolume(float newVolume) => _audioVolumeChanger.SetMasterVolume(newVolume);

        private void SetEffectsVolume(float newVolume) => _audioVolumeChanger.SetEffectsVolume(newVolume);
        
        private void SetOstVolume(float newVolume) => _audioVolumeChanger.SetMusicVolume(newVolume);

        private void OnDestroy() => _slider.onValueChanged.RemoveListener(ChangeValue);

        private enum VolumeType
        {
            Master = 0,
            Ost = 10,
            Effects = 20
        }
    }
}