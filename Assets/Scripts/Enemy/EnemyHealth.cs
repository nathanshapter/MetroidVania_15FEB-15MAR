using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyValues enemyValues;
   public int health;
    public int contactDamage;
    public float knockbackY, knockbackX;
    private void Start()
    {
        health = enemyValues.health;
        contactDamage = enemyValues.contactDamage;
        knockbackY= enemyValues.knockbackY;
        knockbackX= enemyValues.knockbackX;
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
