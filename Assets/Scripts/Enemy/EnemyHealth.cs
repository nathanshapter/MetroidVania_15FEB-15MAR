using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyValues enemyValues;
  [SerializeField]  private int health;
    public int attackdamage;
    private void Start()
    {
        health = enemyValues.health;
        attackdamage = enemyValues.attackDamage;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
