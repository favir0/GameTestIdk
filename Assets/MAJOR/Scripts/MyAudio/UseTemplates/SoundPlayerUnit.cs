using System;
using MyAudio.Core;
using MyAudio.Data;
using UnityEngine;

namespace MyAudio.UseTemplates
{
    public class SoundPlayerUnit : MonoBehaviour
    {
        // ------------------ Settings enums ------------------
        private enum SoundType { Stereo, Mono }
        private enum ClipToUse { Random, Certain }
        private enum CertainClipTypes { Single, Series }
        
        // ------------------ General settings ------------------
        [Header("General sound settings")]
        [SerializeField] private SoundType soundType = SoundType.Stereo;

        [Header("Choose sound clip")] 
        [SerializeField] private ClipGroupsEnum clipGroup;
        [SerializeField] private AnimeGirlsActionsEnum weightsActions;
        [SerializeField] private GeneralSoundsEnum generalSounds;
        
        [Header("Clip to use settings")]
        [SerializeField] private ClipToUse clipToUse = ClipToUse.Random;
        [SerializeField] private CertainClipTypes certainClipTypes = CertainClipTypes.Single;
        [SerializeField] private int clipIndex = 0;
        [SerializeField] private int[] clipIndexSeries;

        [Header("Local sound settings")] 
        [Range (0f, 1f)]
        [SerializeField] private float volumeMultiplier = 1f;
        
        // ------------------ Private fields ------------------
        private int _seriesIndex = -1;
        
        // ------------------ Main logic ----------------------
        
        /// <summary>
        /// Method for outer call to play sound by settings
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void PlaySoundBySettings()
        {
            Enum someEnum = clipGroup == ClipGroupsEnum.General ? generalSounds : weightsActions;
            bool isFlat = soundType != SoundType.Stereo;
            
            switch (clipToUse)
            {
                case ClipToUse.Random:
                    LabSoundEffect.PlayAnySound(clipGroup, someEnum, transform.position, soundVolumeMultiplier: volumeMultiplier, isFlat: isFlat);
                    break;
                case ClipToUse.Certain:
                    int clipIndexToUse = clipIndex;
                    if (clipIndexSeries is { Length: > 1 } && certainClipTypes == CertainClipTypes.Series)
                    {
                        clipIndexToUse = GetNextSeriesIndex();
                    }
                    LabSoundEffect.PlayCertainSound(clipGroup, someEnum, transform.position, clipIndexToUse, soundVolumeMultiplier: volumeMultiplier,isFlat: isFlat);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private int GetNextSeriesIndex()
        {
            if (_seriesIndex < clipIndexSeries.Length - 1)
            {
                _seriesIndex++;
            }
            else
            {
                _seriesIndex = 0;
            }

            return clipIndexSeries[_seriesIndex];
        }
    }
}
