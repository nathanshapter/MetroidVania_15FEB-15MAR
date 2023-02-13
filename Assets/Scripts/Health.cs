using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int playerHealth;
    [SerializeField] int spikeDamage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            TakeDamage(spikeDamage);
            if (CheckDeath()) { ProcessDeath(); }
            
        }
    }
    public int TakeDamage(int damage)
    {
        playerHealth -= damage;
        return playerHealth;
    }
    public bool CheckDeath()
    {
        if(playerHealth >= 0) { return false; }
       
        else { return true; }       
       

    }
    public void ProcessDeath()
    {
        Destroy(gameObject); // many other things to add here
    }
}
