using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    float timeBetweenAttack;
    public float startTimeBetweenAttack;

    public Transform attackPos, attackUpPos, attackDownPos;
    public float attackRange;

    public LayerMask whatIsEnemies;

    private bool swordUp, swordDown, swordOriginal = true;



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
            if(swordUp) 
            {
                print("attacked up");
            }
            if (swordDown) 
            { 
                print("attacked down");
            }
            if(swordOriginal) 
            { 
                print("attacked");
            }
            
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(ProcessAttack(), attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
            }
            timeBetweenAttack = startTimeBetweenAttack;
        }
        
    }
    public void SwordUp(InputAction.CallbackContext context)
    {
        swordOriginal= false;
        if (context.performed) { print("sword up"); }
         swordUp = true;
        if (context.canceled) { swordUp = false; print("sword returned"); swordOriginal = true; }


    }
    public void SwordDown(InputAction.CallbackContext context)
    {
        swordOriginal = false;
        if (context.performed) { print("sword down"); }
        swordDown = true;       
        if (context.canceled) { swordDown = false; print("sword returned");swordOriginal = true; }
        
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;    
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    private Vector2 ProcessAttack()
    {
        if (swordUp)
        {
            return attackUpPos.position;
        }
        if (swordDown)
        {
            return attackDownPos.position;
        }
        else
        {
            return attackPos.position;
        }
    }
}
