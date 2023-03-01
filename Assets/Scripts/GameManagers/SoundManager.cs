using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{  
   
    public static SoundManager Instance { get; private set; }

    private AudioSource source;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

        source= GetComponent<AudioSource>();
    }

  
    public void PlaySound(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }
    public void PauseSound()
    {
        source.Pause();
    }
    public void StopSound()
    {
        source.Stop();
    }
}

