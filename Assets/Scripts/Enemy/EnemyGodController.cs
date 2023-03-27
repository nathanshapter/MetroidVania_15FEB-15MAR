using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGodController : MonoBehaviour
{
    public EnemyValues enemyValues;
    public int contactDamage;
    public float knockbackY, knockbackX;
    PlayerMovement player;
    float knockbackXOriginal;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        contactDamage = enemyValues.contactDamage;
        knockbackY = enemyValues.knockbackY;
        knockbackX = enemyValues.knockbackX;
        knockbackXOriginal = knockbackX;
    }

   
    void Update()
    {
      


        if(transform.position.x < player.transform.position.x)
        {
        knockbackX= knockbackXOriginal;
        }
        if(transform.position.x > player.transform.position.x)
        {
           knockbackX = -knockbackXOriginal;
        }
        
    }
}
