using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("==========Sword Position==========")]
    [Space(20)]
    public Transform attackPos;
    public Transform attackUpPos;
    public Transform attackCrouchPos;
    public Transform attackDownPos;
    public bool swordUp, swordCrouchPosition, swordOriginal = true, swordDown;
    [HideInInspector] public bool isGroundAttacking = false;
    [Space(20)]
    [Header("==========Attack Values==========")]
    [Space(20)]
    public float attackRange;
    public int damage;
    [SerializeField] float attackingSpeed = 1.5f;
    [SerializeField] float timeBetweenAttack; 
    [SerializeField] float slowDownAfterAttack = 0.75f;
   
    [Space(20)]
    public LayerMask whatIsEnemies;
    
   
    // sets in code  
    int currentAttack = 0;
    Animator anim;
    PlayerMovement playerMovement;    
    bool canAttackInAir = true;
    private AudioManager_PrototypeHero audioManager;

    Health health;
    float ballTimer;


    //fireball variables
    [Header("==========Fireball==========")]
    [Space(20)]

    [SerializeField] GameObject fireball;
    [SerializeField] private GameObject InstantiatePos;
    bool hasSpawnedFireBall = false;


    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        audioManager = AudioManager_PrototypeHero.instance;
        health = GetComponent<Health>();
    }

    private void Update()
    {      
        ballTimer +=Time.deltaTime;
        if(ballTimer > 0.07f)
        {
            hasSpawnedFireBall = false;
        }
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
            isGroundAttacking = true;
            playerMovement.speedActuel = attackingSpeed;
            GroundedAttack();
            
        }        
       
      
    }
    private IEnumerator ResetGroundAttacking()
    {
        yield return new WaitForSeconds(slowDownAfterAttack);
        isGroundAttacking = false;
    }

    private void AirAttack()
    {
        if(canAttackInAir && swordUp)
        {
            anim.SetTrigger("UpAttack");
        }
        else if (canAttackInAir && swordOriginal)
        {
            if (timeBetweenAttack > .55f && currentAttack == 0)
            {
                anim.SetTrigger("AirAttack");
                audioManager.PlaySound("SwordAttack");
            }
           
        }
        else if(canAttackInAir && swordDown)
        {
            anim.SetTrigger("AttackAirSlam");
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
            else if (swordCrouchPosition) // ie crouched
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
        StartCoroutine(ResetGroundAttacking());
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

    public void SwordDown(InputAction.CallbackContext context)
    {
       
        if(!playerMovement.IsGrounded())
        {
            swordOriginal = false;
            swordDown= true;
            attackRange = 2.1f;
        }
        if (context.canceled ) // need to set attack range back in IsGrounded
        {
            swordDown= false; swordOriginal = true;
            attackRange = .61f;
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
        if (swordCrouchPosition && playerMovement.IsGrounded())
        {
            return attackCrouchPos.position;
        }
        if (swordDown && !playerMovement.IsGrounded())
        {
            return attackDownPos.position;
        }
        else
        {
            return attackPos.position;
        }
    }
    public bool isParrying;
    public void Parry(InputAction.CallbackContext context)
    {
        if (context.performed && playerMovement.IsGrounded() && !playerMovement.isDashing &&  !playerMovement.SomethingAbove()) 
        {
            anim.SetTrigger("ParryStance"); // eventually to remove cantakeDMG and replace with parrying
            health.canTakeDmg = false;
            isParrying = true;
            Invoke("CanTakeDMG", .5f);
        }
        if(context.canceled)
        {
            health.canTakeDmg = true;
            isParrying = false;
        }
    }
    void CanTakeDMG()
    {
        health.canTakeDmg = true;
        isParrying = false;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Projectile") && isParrying && !hasSpawnedFireBall)
        {
            
            anim.SetTrigger("Parry"); // might have to change this to a bool
            
            Instantiate(fireball, InstantiatePos.transform.position, InstantiatePos.transform.rotation);
         
            ballTimer = 0;
            hasSpawnedFireBall=true;
            
        }
    }
}
