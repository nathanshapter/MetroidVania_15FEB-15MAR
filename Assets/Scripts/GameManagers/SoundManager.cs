using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{  
   
    public static SoundManager Instance { get; private set; }

    private AudioSource musicSource;
    private AudioSource enemyAttackSounds;
    private AudioSource playerAttackSounds;
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

        musicSource= GetComponent<AudioSource>();
        if(isLoadingScreen)
        {
            PlaySound(loadingScreenMusic);
        }
        else { StopSound(); }
    }

  
    public void PlaySound(AudioClip sound)
    {
        musicSource.PlayOneShot(sound);
    }
    public void PauseSound()
    {
        musicSource.Pause();
    }
    public void StopSound()
    {
        musicSource.Stop();
    }
}

