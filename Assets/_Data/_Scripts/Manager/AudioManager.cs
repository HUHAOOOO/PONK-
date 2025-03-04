using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;//de sd array:v
using Random = UnityEngine.Random;
using UnityEngine.UI;
using Unity.VisualScripting;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public Slider sliderVolume;
    float volumeStart = 0.05f;

    private int x;
    //public static AudioManager instance;
    // Use this for initialization
    void Awake()
    {
        //if(instance == null)
        //{
        //    instance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    private void Update()
    {

        if (x == 0)
        {
            SetVolume("Theme");
        }
        else if (x == 1)
        {
            SetVolume("Theme1");
        }
        else if (x == 2)
        {
            SetVolume("Theme2");
        }
        else if (x == 3)
        {
            SetVolume("Theme3");
        }
        else if (x == 4)
        {
            SetVolume("Theme4");
        }
        else if (x == 5)
        {
            SetVolume("Theme5");
        }
    }
    void Start()
    {
        SetMaxvolume(1);
        x = Random.Range(0, 6);
        if (x == 0)
        {
            Play("Theme");//nhac back ground //ma ko bt keo slider kieu j nen bo qua vay:v
        }
        else if (x == 1)
        {
            Play("Theme1");
        }
        else if (x == 2)
        {
            Play("Theme2");
        }
        else if (x == 3)
        {
            Play("Theme3");
        }
        else if (x == 4)
        {
            Play("Theme4");
        }
        else if (x == 5)
        {
            Play("Theme5");
        }

    }
    public void Play(string name)
    {
        Debug.Log(name + "!!!sound");
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!!");
            return;
        }
        //Debug.Log("sound : "+s.source);
        //Debug.Log(" mute "+s.source.mute);
        //Debug.Log(" volume: "+s.source.volume);
        s.source.Play();

        //s.source.Stop();Debug.Log("Da Stop source");

    }
    //=====================================
    public void SetMaxvolume(int volume)//dat gia tri slider
    {
        sliderVolume.maxValue = volume;
        sliderVolume.value = volumeStart;
    }
    //public void SetVolume(string name)
    //{
    //    float volume = sliderVolume.value;Debug.Log("volume = sliderVolume.value;");
    //    Sound s = Array.Find(sounds, sound => sound.name == name);
    //    if(s == null)
    //    {
    //        Debug.Log("s NULL");
    //    }
    //    s.volume = volume;
    //}
    public void SetVolume(string name)//lay gtri slider
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("s NULL");
        }
        s.source.volume = sliderVolume.value;
    }
}
