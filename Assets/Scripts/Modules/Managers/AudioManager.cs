using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using DG.Tweening;

public partial class AudioManager : SingletonNoMono<AudioManager>
{
    private GameObject audioManager;
    private const int effectSourcePoolSize = 8;
    private List<AudioSource> effectSourcePool = new List<AudioSource>();
    private AudioSource musicSource;
    private string currentBGMPath = "";
    // private Dictionary<AudioSource, float> originalVolume = new Dictionary<AudioSource, float>();
    private float musicOriginalVolume;
    public bool banBgm { get; private set; }
    public float globalVolume { get; private set; }

    public void SetBanBgm(bool value)
    {
        banBgm = value;
        if (value)
        {
            musicSource.mute = true;
        }
        else
        {
            musicSource.mute = false;
            PlayBGMDontStop_ForSO();
        }
        SaveManager.instance.SetBanBgmSave(!value);
    }

    public void SetGlobalVolume(float value)
    {
        globalVolume = value;
        SetAudioSource(musicSource, null, true, musicOriginalVolume * globalVolume);
        SaveManager.instance.SetGlobalVolumeSave(value);
    }

    public AudioManager()
    {
        globalVolume = SaveManager.instance.GetGlobalVolumeSave();
        banBgm = SaveManager.instance.GetBanBgmSave();
        audioManager = GameObject.Find("AudioManager");
        if (audioManager == null)
        {
            audioManager = new GameObject("AudioManager");
        }
        Object.DontDestroyOnLoad(audioManager);
        musicSource = audioManager.AddComponent<AudioSource>();
        musicSource.loop = true;
        for (int i = 0; i < effectSourcePoolSize; ++i)
        {
            effectSourcePool.Add(audioManager.AddComponent<AudioSource>());
        }
    }

    // public void PlayBGM(string path = "", bool isLoop = true)
    // {
    //     if(path == "")
    //     {
    //         musicSource.Play();
    //         return;
    //     }
    //     SetAudioSource(musicSource, path, isLoop);
    //     musicSource.Play();
    //     currentBGMPath = path;
    // }
    
    // public void PlayBGMDontStop(string path = "", bool isLoop = true)
    // {
    //     if (path == currentBGMPath && musicSource.isPlaying)
    //     {
    //         return;
    //     }
    //     if(path == "")
    //     {
    //         musicSource.Play();
    //         return;
    //     }
    
    //     SetAudioSource(musicSource, path, isLoop, 0.15f * globalVolume);
    //     musicSource.Play();
    //     currentBGMPath = path;
    // }

    public void PlayEffectAudio(string path)
    {
        foreach(var item in effectSourcePool)
        {
            if(item.isPlaying == false)
            {
                SetAudioSource(item, path, false, 0.7f * globalVolume);
                item.Play();
                break;
            }
        }
    }

    public void StopBGM()
    {
        musicSource.Stop();
    }

    public void StopBGMFadeOut()
    {
        if(musicSource.isPlaying == false)
        {
            return;
        }
        musicSource.DOFade(0, 2f);
    }

    private void SetAudioSource(AudioSource source, string path = "", bool isLoop = false, float volume = 0.15f)
    {
        path = "Audio/" + path;
        var audio = Resources.Load<AudioClip>(path);
        if(audio == null)
        {
            Debug.LogError("no sound under Resources folder path");
        }
        else
        {
            source.clip = Resources.Load<AudioClip>(path);
        }
        source.loop = isLoop;
        source.volume = volume;
    }
}
