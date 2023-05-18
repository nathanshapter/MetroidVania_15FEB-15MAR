using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour, iSaveData
{
   
  [SerializeField]  CheckpointsManager cpManager;
    [SerializeField] Health health;    
    [SerializeField] public Transform[] spawnPositions; // used for when lives go to 0  
    Scene deathSceneToReloadTo;



    private void Awake()
    {              
        health = FindObjectOfType<Health>();
        
    }
 
    
    public void LoadData(GameData gameData)
    {

    }
    public void SaveData(GameData gameData)
    {
        gameData.sceneToReloadTo = deathSceneToReloadTo.name;    
        gameData.deathCount = DeathManager.Instance.totalDeaths;
    }
    public void RespawnPlayer()
    {
        
        DeathManager.Instance.totalDeaths++;     
       
        deathSceneToReloadTo  = SceneManager.GetActiveScene();  
        Invoke("LoadDeathScene", 3); 
        
        
    
        health.playerHealth = health.amountOfLives;


       

        SaveDataManager.instance.SaveGame();
        print("amount of deaths saved" + DeathManager.Instance.totalDeaths ); // leave this print in until its fixed
    }
   
    private void LoadDeathScene()
    {

        SceneManager.LoadScene("DeathScene");
       
    }
    public Transform returnTransformPosition() // i dont remember what this does just let it be
    {

        return cpManager.lastCheckPointPos; 
    }
  
}
