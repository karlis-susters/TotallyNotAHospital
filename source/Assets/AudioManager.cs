using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
        
    }
    public void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Play("DayStart");
        }
        else
        {
            Play("MainMenuMusic");
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        Debug.Log("Playing " + name);
        s.source.Play();

    }
}
