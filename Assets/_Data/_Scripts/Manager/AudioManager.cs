using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;//de sd array:v
using Random = UnityEngine.Random;
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;

    public Sound[] musicSounds, sfxSounds;

    public Sound currentMusicSounds;
    public Sound currentSFXSounds;
    public float deffaultVolume = 0.1f;
    public float deffaultPitch = 1f;

    private int xToRandomTheme;
    void Awake()
    {
        if (instance != null) Debug.LogError("only allow 1 AudioManager | Singleton");
        AudioManager.instance = this;


        //if(instance == null)
        //{
        //    instance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}


        // musicSounds
        foreach (Sound s in musicSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume = deffaultVolume;
            s.source.pitch = s.pitch = deffaultPitch;
            s.source.loop = s.loop;
        }

        // sfxSounds
        foreach (Sound s in sfxSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume = deffaultVolume;
            s.source.pitch = s.pitch = deffaultPitch;
            s.source.loop = s.loop;
        }
    }
    void Start()
    {
        this.PlayMusic("Minecraft_Calm");
    }
    public void PlayMusic(string name)
    {
        //Debug.Log(name + "!!!musicSounds");
        Sound s = Array.Find(musicSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("musicSounds : " + name + "not found!!");
            return;
        }
        currentMusicSounds = s;
        s.source.Play();

        //s.source.Stop();Debug.Log("Da Stop source");
        // ~ AudioSource.Stop : stop 

    }

    public void PlaySFX(string name)
    {
        //Debug.Log(name + "!!!sfxSounds");
        Sound s = Array.Find(sfxSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("sfxSounds : " + name + "not found!!");
            return;
        }
        currentSFXSounds = s;
        //s.source.Play();
        s.source.PlayOneShot(s.clip);
    }

    public void ToggleMusic()
    {
        //AudioSource.mute = !AudioSource.mute;
        currentMusicSounds.source.mute = !currentMusicSounds.source.mute;

    }
    public void ToggleSFX()
    {
        //AudioSource.mute = !AudioSource.mute;
        //currentSFXSounds.source.mute = !currentSFXSounds.source.mute;
        foreach (Sound s in sfxSounds)
        {
            s.source.mute = !s.source.mute;
        }
    }

    public void MusicVolume(float volume)
    {
        //AudioSource.volume = volume;
        currentMusicSounds.source.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        //AudioSource.volume = volume;
        //currentSFXSounds.source.volume = volume;

        foreach (Sound s in sfxSounds)
        {
            s.source.volume = volume;
        }

    }




    public string RandomSoundSword()
    {
        int random = Random.Range(0, 4);

        if (random == 0) return "Sword Swing 1";
        if (random == 1) return "Sword Swing 2";
        if (random == 2) return "Sword Swing 3";

        return "Sword Swing 1";
    }


}
