using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour, iSaveData
{
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

    public void ProcessDeath()
    {
        totalDeaths++;
        
        
       
    }
    public void RespawnPlayer(Transform respawnPosition)
    {
        if (respawnPosition == null)
        {
            player.transform.position = RespawnManager.spawnPositions[0].transform.position;
           
            return;
        }  
        if(fallRespawn) { player.transform.position = respawnPosition.transform.position; }
        
    }


    public IEnumerator InitiateRespawn()
    {
        yield return new WaitForSeconds(1);
        RespawnPlayer(RespawnManager.returnTransformPosition());
    }
    
}
