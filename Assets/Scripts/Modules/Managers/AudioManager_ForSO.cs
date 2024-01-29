using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AudioManager
{
    public void PlayBGMDontStop_ForSO(SO_AudioConfig config = null, bool isLoop = true)
    {
        if (config == null || config.resourcePath == "") 
        {
            musicSource.Play();
            return;
        }
        if (config.resourcePath == currentBGMPath && musicSource.isPlaying)
        {
            return;
        }

        musicOriginalVolume = config.volume;
        SetAudioSource(musicSource, config.resourcePath, isLoop, musicOriginalVolume * globalVolume);
        if (!banBgm)
        {
            musicSource.Play();
        }
        currentBGMPath = config.resourcePath;
    }

    public void PlayEffectAudio_ForSO(SO_AudioConfig config)
    {
        foreach(var item in effectSourcePool)
        {
            if(item.isPlaying == false)
            {
                SetAudioSource(item, config.resourcePath, false, config.volume * globalVolume);
                item.Play();
                break;
            }
        }
    }
}
