using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("==========Sets==========")]

    [SerializeField] float timeBetweenAttack;
    [SerializeField] AudioClip swing;
    public Transform attackPos, attackUpPos, attackDownPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;

    // sets in code

    private bool swordUp, swordDown, swordOriginal = true;
    bool hasAttackedTwice = false;
    Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {       
       timeBetweenAttack += Time.deltaTime; 
    }  
    
    public void Attack(InputAction.CallbackContext context)
    {
     if(timeBetweenAttack < 1 && timeBetweenAttack > 0.4f) // logic to allow combo
        {
            if (hasAttackedTwice) { return; }
            else { print("hi"); hasAttackedTwice = true; anim.SetTrigger("Attack2"); }            
        }  
        
        if (timeBetweenAttack > 1)
        {
            hasAttackedTwice = false;
            anim.SetTrigger("Attack1");
          //  SoundManager.Instance.StopSound();
          //  SoundManager.Instance.PlaySound(swing);
           
            if (swordUp)  /// these need to do something other than print
            {
               
            }
            if (swordDown) 
            { 
               
            }
            if(swordOriginal) 
            { 
               
            }
            
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(ProcessAttack(), attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                if (enemiesToDamage[i].GetComponent<EnemyHealth>() == null) // for cerberus heards // body
                {
                    enemiesToDamage[i].GetComponentInParent<EnemyHealth>().TakeDamage(damage);
                }
                else
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                }

              
            }
            timeBetweenAttack = 0;
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
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;    
        Gizmos.DrawWireSphere(ProcessAttack(), attackRange);
        
    }
    private Vector2 ProcessAttack() // returns attack position
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
