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

   
    // this script needs to identify the starting position and spawn them at the correct one, not just position [0]
    private void Awake()
    {

       
        deathCounter = FindObjectOfType<DeathCounter>();
        
    }
    private void Update()
    {
        // to remove once better checks are put in place

        if(health.transform.position.y <= -100)
        {
            RespawnPlayer();
        }
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
