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
   [SerializeField] int currentAttack = 0;
    Animator anim;
    PlayerMovement playerMovement;
    float attackSpamPoint; // 

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {       
       timeBetweenAttack += Time.deltaTime; 
        if(timeBetweenAttack > .75f)
        {
         //   currentAttack= 0;
        }
    }
    
    public void Attack(InputAction.CallbackContext context)
    {

        if (!playerMovement.IsGrounded())
        {
            AirAttack();
            
            
        }
        else if(playerMovement.IsGrounded())
        {
            GroundedAttack();
            
        } 
        
      
    }

    private void AirAttack()
    {
        AllSwordAttack();
        anim.SetTrigger("AirAttack");
       
    }
    private void GroundedAttack()
    {
        
        if (timeBetweenAttack > .55f && currentAttack ==0)
        {
            currentAttack++;
            anim.SetTrigger("Attack1");           

            if (swordUp)  /// these need to do something other than print
            {

            }
            if (swordDown)
            {

            }
            if (swordOriginal)
            {

            }

        }
        if (timeBetweenAttack < .55f && timeBetweenAttack > 0.17f && currentAttack ==1) // logic to allow combo
        {
          currentAttack++; // this will be used later for a 3rd attack
            

                anim.SetTrigger("Attack2");
                currentAttack = 0;
            

        }
        AllSwordAttack();
    }

    private void AllSwordAttack() // all attacks run this
    {
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

     //  if(timeBetweenAttack > .55f) // need to eventually add a fatigue option so cant spam this forever
        {
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
