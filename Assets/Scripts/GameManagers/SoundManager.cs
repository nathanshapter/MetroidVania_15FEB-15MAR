using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{  
   
    public static SoundManager Instance { get; private set; }

    private AudioSource source;
    [SerializeField] bool isLoadingScreen;
    [SerializeField] AudioClip loadingScreenMusic;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

        source= GetComponent<AudioSource>();
        if(isLoadingScreen)
        {
            PlaySound(loadingScreenMusic);
        }
        else { StopSound(); }
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

