using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioFile[] AudioFiles;
    public AudioFile[] MusicFiles => AudioFiles.Where(x => x.Type == AudioType.Music).ToArray();
    public AudioSource MusicSource;
    public AudioSource Player;
    public List<AudioSource> AudioSourcesSfx;
    public AudioMixer AudioMixer;

    [Range(0, 1)]
    public float GlobalMusicVolume = 1;
    [Range(0, 1)]
    public float GlobalSFXVolume = 1;

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        Instance = this;
    }

    public static void PlayMusic(string name)
    {
        Instance._PlayMusic(name);
    }

    public static void PlayRandomMusic()
    {
        Instance._PlayRandomMusic();
    }

    public static void PlaySFX(string name)
    {
        Instance._PlaySFX(name);
    }

    public static void PlaySFX(string name, AudioSource audioSource)
    {
        Instance._PlaySFX(name, audioSource);
    }

    public static void StopSFX(string name)
    {
        Instance._StopSFX(name);
    }

    public static bool IsSoundPlaying(string name)
    {
        return Instance._IsSoundPlaying(name);
    }

    private void _PlayMusic(string name)
    {
        var clip = GetFileByName(name);
        if (clip != null)
        {
            float vol = GlobalMusicVolume * clip.Volume;
            MusicSource.volume = vol;
            MusicSource.clip = clip.Clip;
            MusicSource.Play();
        }
        else
        {
            Debug.LogError(" No such audio file " + name);
        }
    }

    private void _StopSFX(string name)
    {
        foreach (AudioSource audio in AudioSourcesSfx)
        {
            if (audio.clip != null && audio.clip.name == name && audio.isPlaying)
            {
                 audio.Stop(); 
            }
        }
    }

    private void _PlayRandomMusic()
    {
        var musics = MusicFiles;
        int rdm = Random.Range(0, musics.Length);
        _PlayMusic(musics[rdm].Name);
    }

    private void _PlaySFX(string name)
    {
        AudioSource currentAudioSource = null;

        foreach (AudioSource audio in AudioSourcesSfx)
        {
            if (!audio.isPlaying)
            {
                currentAudioSource = audio;
                break;
            }

        }

        if (currentAudioSource == null)
        {
            return;
        }

        var clip = GetFileByName(name);
        if (clip != null)
        {
            float vol = GlobalSFXVolume * clip.Volume;
            currentAudioSource.volume = vol;
            currentAudioSource.clip = clip.Clip;
            currentAudioSource.outputAudioMixerGroup = AudioMixer.FindMatchingGroups(name).Length > 0 ?
                   AudioMixer.FindMatchingGroups(name)[0] :
                   AudioMixer.FindMatchingGroups("Sfx")[0];

            currentAudioSource.Play();
        }
        else
        {
            Debug.LogError(" No such audio file " + name);
        }
    }

    private void _PlaySFX(string name, AudioSource audioSource)
    {
        if (audioSource.isPlaying) return;

        var clip = GetFileByName(name);
        if (clip != null)
        {
            float vol = GlobalSFXVolume * clip.Volume;
            audioSource.outputAudioMixerGroup = AudioMixer.FindMatchingGroups(name).Length > 0 ?
                    AudioMixer.FindMatchingGroups(name)[0] :
                    AudioMixer.FindMatchingGroups("Sfx")[0];
            audioSource.volume = vol;
            audioSource.clip = clip.Clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError(" No such audio file " + name);
        }
    }

    private AudioFile GetFileByName(string name)
    {
        return AudioFiles.First(x => x.Name == name);
    }

    private bool _IsSoundPlaying(string name)
    {
        foreach (AudioSource audio in AudioSourcesSfx)
        {
            if (audio.clip != null && audio.clip.name == name && audio.isPlaying)
            {
                return true;
            }
        }

        return false;
    }
}

[Serializable]
public class AudioFile
{
    public string Name;
    public AudioClip Clip => Clips[Random.Range(0, Clips.Length)];
    public AudioClip[] Clips;
    [Range(0, 1)]
    public float Volume;
    public AudioType Type;
}

public enum AudioType
{
    Music,
    SfX,
    Envirmonment
}