using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int playerHealth, amountOfLives = 5;
    [SerializeField] int spikeDamage;
    [SerializeField] float invincibleTimer = 1f, invincibleTimerOriginal =1f;
    
    [SerializeField] GameObject playerPrefab;
  [SerializeField]  DeathManager deathManager;
    [SerializeField] RespawnManager respawnManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            TakeDamage(spikeDamage);
            deathManager.fallRespawn = true;
            if (!CheckIfAlive()) { deathManager.ProcessDeath();  } // add death screen
            
        }
    }

    private void Update()
    {
        invincibleTimer -= Time.deltaTime;
    }
    public int TakeDamage(int damage)
    {
        if(invincibleTimer > 0) { return playerHealth; }
        playerHealth -= damage;
        invincibleTimer = invincibleTimerOriginal;
        return playerHealth;
    }
    public bool CheckIfAlive()
    {
        if(playerHealth <= 0) { respawnManager.RespawnPlayer(); return false; }
       
        else { StartCoroutine(deathManager.InitiateRespawn()); return true; }     
       

    }
    
}
