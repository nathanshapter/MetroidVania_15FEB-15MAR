using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public int totalDeaths;
   [SerializeField] RespawnManager RespawnManager;
    [SerializeField] GameObject player;
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
       
    }
    private void Update()
    {
       
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
