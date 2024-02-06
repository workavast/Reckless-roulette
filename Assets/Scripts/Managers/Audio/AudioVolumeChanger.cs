using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioVolumeChanger
    {
        private readonly AudioMixer _mixer;

        private const string MasterParam = "Master";
        private const string EffectsParam = "Effects";
        private const string MusicParam = "Music";

        public float MasterVolume = 0.75f;
        public float MusicVolume = 0.75f;
        public float EffectsVolume = 0.75f;
        
        public AudioVolumeChanger(AudioMixer mixer)
        {
            _mixer = mixer;
        }
        
        public void StartInit()
        {
            SetVolume(MasterParam, MasterVolume);
            SetVolume(EffectsParam, EffectsVolume);
            SetVolume(MusicParam, MusicVolume);
        }

        
        public void SetMasterVolume(float newVolume)
        {
            MasterVolume = newVolume;
            SetVolume(MasterParam, MasterVolume);
        }

        public void SetEffectsVolume(float newVolume)
        {
            EffectsVolume = newVolume;
            SetVolume(EffectsParam, EffectsVolume);
        }

        public void SetMusicVolume(float newVolume)
        {
            MusicVolume = newVolume;
            SetVolume(MusicParam, MusicVolume);
        }

        private void SetVolume(string paramName, float newVolume)
            => _mixer.SetFloat($"{paramName}", Mathf.Lerp(-80, 0, Mathf.Sqrt(Mathf.Sqrt(newVolume))));
    }
}