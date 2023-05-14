using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour, iSaveData
{
   
  [SerializeField]  CheckpointsManager cpManager;
    [SerializeField] Health health;    
    [SerializeField] public Transform[] spawnPositions; // used for when lives go to 0
    DeathCounter deathCounter;
   

   
    // this script needs to identify the starting position and spawn them at the correct one, not just position [0]
    private void Awake()
    {

       
        deathCounter = FindObjectOfType<DeathCounter>();
        health = FindObjectOfType<Health>();
        
    }
    private void Update()
    {
        // to remove once better checks are put in place

        if(health.transform.position.y <= -100)
        {
            RespawnPlayer();
        }
    }
    Scene deathSceneToReloadTo;
    public void LoadData(GameData gameData)
    {

    }
    public void SaveData(GameData gameData)
    {
        gameData.sceneToReloadTo = deathSceneToReloadTo.name;
        print(deathSceneToReloadTo.name);
        print(gameData.sceneToReloadTo);
    }
    public void RespawnPlayer()
    {
        deathCounter.totalDeaths++;

        DeathManager.Instance.totalDeaths++;
        
       
        deathSceneToReloadTo  = SceneManager.GetActiveScene();
        print(deathSceneToReloadTo.name);
        Invoke("LoadDeathScene", 3); 
        
        
        print(" hades brought you back");
        health.playerHealth = health.amountOfLives;


        //   health.transform.position = spawnPositions[0].transform.position;
        // restart zone to level 1

        SaveDataManager.instance.SaveGame();
    }
   
    private void LoadDeathScene()
    {

        SceneManager.LoadScene("DeathScene");
       
    }
    public Transform returnTransformPosition()
    {

        return cpManager.lastCheckPointPos; 
    }
  
}
