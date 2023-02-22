using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    private static RespawnManager instance;
  [SerializeField]  CheckpointsManager cpManager;
    [SerializeField] Health health;    
    [SerializeField] public Transform[] spawnPositions; // used for when lives go to 0
    DeathCounter deathCounter;
   

    private void Awake()
    {
        if(instance == null) { instance= this; DontDestroyOnLoad(this); }
        else { Destroy(gameObject); }
        deathCounter = FindObjectOfType<DeathCounter>();
        
    }

    public void RespawnPlayer()
    {
        deathCounter.totalDeaths++;
        print(" hades brought you back");
        health.playerHealth = health.amountOfLives;
        health.transform.position = spawnPositions[0].transform.position;
        // restart zone to level 1
        

    }

    public Transform returnTransformPosition()
    {

        return cpManager.lastCheckPointPos; 
    }
}
