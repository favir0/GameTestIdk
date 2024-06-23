using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyAudio.Data
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "ScriptableObjects/AudioDB", order = 1)]
    public class AudioScrObjDB : ScriptableObject
    {
        [Header("Anime")] 
        public AudioClip[] girlTap1;
        public AudioClip[] girlTap2;
        public AudioClip[] completeStage;

        [Header("General sound")] 
        public AudioClip[] buttonClick;
        public AudioClip[] walletConnect;

        private Dictionary<ClipGroupsEnum, Dictionary<Enum, AudioClip[]>> _clipsByGroup = new Dictionary<ClipGroupsEnum, Dictionary<Enum, AudioClip[]>>();

        // --------------------------------------- LOGIC ---------------------------------------
        public void Init()
        {
            InitializeClipsDictionary();
        }

        private void InitializeClipsDictionary()
        {
            _clipsByGroup = new Dictionary<ClipGroupsEnum, Dictionary<Enum, AudioClip[]>>
            {
                { ClipGroupsEnum.AnimeGirls, new Dictionary<Enum, AudioClip[]>
                    {
                        { AnimeGirlsActionsEnum.GirlTap1, girlTap1 },
                        { AnimeGirlsActionsEnum.GirlTap2, girlTap2 },
                        { AnimeGirlsActionsEnum.CompleteStage, completeStage }
                    }
                },
                { ClipGroupsEnum.General, new Dictionary<Enum, AudioClip[]>
                    {
                        { GeneralSoundsEnum.ButtonClick, buttonClick },
                        { GeneralSoundsEnum.WalletConnect, walletConnect }
                    }
                }
            };
        }

        public AudioClip[] GetAudioClipsByType(ClipGroupsEnum group, Enum clipType)
        {
            if (_clipsByGroup.ContainsKey(group) && _clipsByGroup[group].ContainsKey(clipType))
            {
                return _clipsByGroup[group][clipType];
            }

            return null;
        }
    }

    // --------------------------------------- ENUMS ---------------------------------------
    public enum ClipGroupsEnum
    {
        AnimeGirls, General
    }

    public enum AnimeGirlsActionsEnum
    {
        GirlTap1, GirlTap2, CompleteStage
    }

    public enum GeneralSoundsEnum
    {
        ButtonClick, WalletConnect
    }
}
