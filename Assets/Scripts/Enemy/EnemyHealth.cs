using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyValues enemyValues;
   public int health;
    
    
    private void Start()
    {
        health = enemyValues.health;
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die() // do many other things
    {
        Destroy(gameObject);
    }

}
