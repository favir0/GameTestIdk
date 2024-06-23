using System;
using MyAudio.Data;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace MyAudio.Core
{
    [RequireComponent(typeof(AudioSource))]
    public class LabSoundEffect : Singleton<LabSoundEffect>
    {
        [SerializeField] private AudioScrObjDB _audioScrObjDB;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        
        private const string MUSIC_KEY = "MasterGroup";
        
        private void Awake ()
        {
            InitSingleton(this);
            _audioScrObjDB.Init();
        }

        // Mixer default dB volume formula: Mathf.Log(Mathf.Clamp(value, 0.001f, 1f)) * 20
        // So the inverse formula is Mathf.Exp(mixerVolume / 20)
        private float GetNormalizedMixerVolume()
        {
            _audioMixerGroup.audioMixer.GetFloat(MUSIC_KEY, out float mixerVolume);
            var normalizedVolume = Mathf.Exp(mixerVolume / 20);
            if (normalizedVolume < 0.01f)
                normalizedVolume = 0;
            return normalizedVolume;
        }
        
        public static AudioScrObjDB audioScrObjDB
        {
            get => instance ? instance._audioScrObjDB : null;
            set => instance._audioScrObjDB = value;
        }

        public static AudioSource audioSource 
        {
            get => instance._audioSource;
            set => instance._audioSource = value;
        }

        private static float normalizedSoundVolume => instance.GetNormalizedMixerVolume();

        public static void PlayCertainSound(ClipGroupsEnum group, Enum clipType, Vector3 position, int certainClipIndex, float soundVolumeMultiplier = 1f, bool isFlat = false)
        {
            if (!audioScrObjDB) return;
            AudioClip[] clips = audioScrObjDB.GetAudioClipsByType(group, clipType);
            
            if (clips == null)
            {
                Debug.LogWarning($"No audio clips found for group {group} and type {clipType}");
                return;
            }

            AudioClip clipToPlay = GetClipAtIndex(clips, certainClipIndex);
            if (clipToPlay != null)
            {
                PlaySound(clipToPlay: clipToPlay, soundVolumeMultiplier: soundVolumeMultiplier, position: position, isFlat: isFlat);
            }
        }
        
        public static void PlayAnySound(ClipGroupsEnum group, Enum clipType, Vector3 position, float soundVolumeMultiplier = 1f, bool isFlat = false)
        {
            if (!audioScrObjDB) return;
            AudioClip[] clips = audioScrObjDB.GetAudioClipsByType(group, clipType);
            if (clips == null)
            {
                Debug.LogWarning($"No audio clips found for group {group} and type {clipType}");
                return;
            }

            AudioClip clipToPlay = GetRandomClip(clips);
            if (clipToPlay != null)
            {
                PlaySound(clipToPlay: clipToPlay, soundVolumeMultiplier: soundVolumeMultiplier, position: position, isFlat: isFlat);
            }
        }

        private static AudioClip GetRandomClip(AudioClip[] audioClipArr)
        {
            return audioClipArr[Random.Range(0, audioClipArr.Length)];
        }

        private static AudioClip GetClipAtIndex(AudioClip[] audioClipArr, int clipIndex)
        {
            if (clipIndex > -1 && clipIndex < audioClipArr.Length)
            {
                return audioClipArr[clipIndex];
            }

            Debug.LogWarning($"Clip index {clipIndex} is out of range");
            return null;
        }

        private static void PlaySound(AudioClip clipToPlay, float soundVolumeMultiplier = 1f, Vector3 position = default, bool isFlat = false)
        {
            if (clipToPlay == null)
            {
                Debug.LogWarning("Trying to play null audio clip");
                return;
            }
            
            // Flat sound check
            if (isFlat)
            {
                audioSource.PlayOneShot(clipToPlay);
            }
            else
            {
                float tempVolume = normalizedSoundVolume * soundVolumeMultiplier;
                float soundVolumeToUse = Mathf.Clamp(tempVolume, 0, 1);
                AudioSource.PlayClipAtPoint(clipToPlay, position, soundVolumeToUse);
            }
        }
        
        private void OnDestroy () 
        {
            ClearSingleton();
            AudioCleanup.Cleanup();
        }
    }
}
