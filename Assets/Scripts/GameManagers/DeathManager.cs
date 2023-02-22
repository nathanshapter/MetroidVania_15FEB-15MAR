using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] int totalDeaths;
   [SerializeField] RespawnManager RespawnManager;
    [SerializeField] GameObject player;
    public bool fallRespawn;
  
  

    public void ProcessDeath()
    {
        totalDeaths++;     
       
    }
    private void RespawnPlayer(Transform respawnPosition)
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
