using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyValues enemyValues;

    private int health, attackdamage;


    private void Start()
    {
       health = enemyValues.health;
        attackdamage = enemyValues.attackDamage;
        
       
    }
}
