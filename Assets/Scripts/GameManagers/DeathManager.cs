using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour, iSaveData
{
    
    /// <summary>
    /// this script handles what happens when the player dies
    /// </summary>


    public int totalDeaths;
   [SerializeField] RespawnManager RespawnManager;
    [SerializeField] PlayerMovement player;
   [HideInInspector] public bool fallRespawn;
    


    public static DeathManager Instance;
 

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

        RespawnManager = FindObjectOfType<RespawnManager>();
        player= FindObjectOfType<PlayerMovement>();
       
    }
 public void LoadData(GameData data)
    {
        RespawnManager = FindObjectOfType<RespawnManager>();
        player = FindObjectOfType<PlayerMovement>();
        this.totalDeaths = data.deathCount;   
    }
    public void SaveData(GameData data)
    {
        data.deathCount = this.totalDeaths;
      
    }

  
    public void RespawnPlayer(Transform respawnPosition)
    {
        if (respawnPosition == null)
        {
            player.transform.position = RespawnManager.spawnPositions[0].transform.position;
           
            return;
        }  
        if(fallRespawn)  // this is for when the player lands on spikes
        { 
            player.transform.position = respawnPosition.transform.position; 
        }
        
    }


    public IEnumerator InitiateRespawn()
    {
        yield return new WaitForSeconds(1);
        RespawnPlayer(RespawnManager.returnTransformPosition());
    }
    
}
