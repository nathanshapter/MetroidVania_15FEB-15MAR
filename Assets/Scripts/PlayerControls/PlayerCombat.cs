using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    float timeBetweenAttack;
    public float startTimeBetweenAttack;

    public Transform attackPos;
    public float attackRange;

    public LayerMask whatIsEnemies;





    //damage values
    public int damage;

    private void Update()
    {
       
       timeBetweenAttack -= Time.deltaTime; 
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if (timeBetweenAttack <= 0)
        {
            
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyController>().TakeDamage(damage);
            }
            timeBetweenAttack = startTimeBetweenAttack;
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;    
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
