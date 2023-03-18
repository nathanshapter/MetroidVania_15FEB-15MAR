using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("==========Sword Position==========")]

    public Transform attackPos;
    public Transform attackUpPos;
    public Transform attackDownPos;
    public bool swordUp, swordDown, swordOriginal = true;
    [Space(20)]
    public float attackRange;
    public int damage;
    [SerializeField] float attackingSpeed = 1.5f;
    [SerializeField] float timeBetweenAttack; 
    [SerializeField] float slowDownAfterAttack = 0.75f;
    [SerializeField] AudioClip swing;
    [Space(20)]
    public LayerMask whatIsEnemies;
    
   
    // sets in code  
    int currentAttack = 0;
    Animator anim;
    PlayerMovement playerMovement;    
    bool canAttackInAir = true;

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
            currentAttack= 0;
        }

        if (playerMovement.IsGrounded()) // this needs to be put somewhere better later
        {
            canAttackInAir = true;
        }

        if(timeBetweenAttack >slowDownAfterAttack  && playerMovement.speedActuel == attackingSpeed)
        {
            playerMovement.speedActuel = playerMovement.originalSpeed;
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
            playerMovement.speedActuel = attackingSpeed;
            GroundedAttack();
            
        }        
       
      
    }

    private void AirAttack()
    {
        if(canAttackInAir && swordUp)
        {
            anim.SetTrigger("UpAttack");
        }
        else if (canAttackInAir && swordOriginal)
        {            
            anim.SetTrigger("AirAttack");            
        }
        AllSwordAttack();
        currentAttack++;

    }
    private void GroundedAttack()
    {        
        if (timeBetweenAttack > .55f && currentAttack ==0)
        {
            currentAttack++;
            if (swordUp)  
            {
                anim.SetTrigger("UpAttack");
            }
            else if (swordDown) // ie crouched
            {

            }
            else if (swordOriginal)
            {
                anim.SetTrigger("Attack1");
            }

        }
        else if (timeBetweenAttack < .55f && timeBetweenAttack > 0.17f && currentAttack ==1 && swordOriginal) // logic to allow combo
        {
          currentAttack++; // this will be used later for a 3rd attack
            

                anim.SetTrigger("Attack2");
                
            

        }
       else if(currentAttack == 2 && timeBetweenAttack >= .15f && swordOriginal) // logic to allow 3rd swing
        {
            anim.SetTrigger("Attack1"); // to be replaced eventually with attack 3
            currentAttack= 0;
            
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

        if (!playerMovement.IsGrounded() && currentAttack != 0)
        {
            canAttackInAir = false;
        }      


       if(currentAttack != 0 && timeBetweenAttack > 0.1f) // need to eventually add a fatigue option so cant spam this forever
        {
            timeBetweenAttack = 0;
        }       
            
        
       
    }



    public void SwordUp(InputAction.CallbackContext context)
    {
        swordOriginal= false;
        if (context.performed) { }
         swordUp = true;
        if (context.canceled) { swordUp = false;  swordOriginal = true; }


    }
  
    private void OnDrawGizmosSelected() 
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
