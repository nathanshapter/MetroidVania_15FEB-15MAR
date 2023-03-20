using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    // script Gets
    PlatformBullet platformBullet;
    Health health;
    ProgressionManager progressionManager;
    RespawnManager respawnManager;
    PlayerCombat playerCombat;

    // component Gets
    Rigidbody2D rb;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    [SerializeField] GameObject groundCheck;
    [SerializeField] GameObject roofCheck;
    CapsuleCollider2D capsuleCollider;
    BoxCollider2D boxCollider2D;
    PlayerInput playerInput;
    WallGrabPlayer wallGrab;
    

    Animator animator;

    [Header("=========== Movement Floats ==========")]
    [Space(20)]
    
    public float speedActuel;
    public float walkingSpeed;
    [HideInInspector] public float originalSpeed;
    [HideInInspector] public float horizontal;
    float coyoteTime = 0.5f, coyoteTimeCounter;
    

    //jumping
    [SerializeField] float jumpingPower;
    [Range(1f, 2f)][SerializeField] float shrunkJumpingPower;

    [Header("=====Dash Values=====")]
    [Space(20)]
    [SerializeField] float dashingPower;
    [SerializeField] float dashingTime;
    [SerializeField] float dashingCooldown;
    [Space(20)]



    // to add, timer in air to determine hard landing
    // movement bool
    public bool isCrouching = false;
    [HideInInspector] public bool isFacingRight = true;   
    [HideInInspector] public  bool canDash = true, isDashing;
    bool hasDoubleJumped = true;
    bool isDead = false;
    bool walk = false;
    bool isMoving = false;
    bool allowDoubleWallJump = false;
    bool isTouchingBridge;

    [Header("=====Misc=====")]
    
    [Space(20)]
    
    [HideInInspector] public bool fluteIsPlaying;
    [SerializeField] float fluteCooldown;
    
    [SerializeField] TrailRenderer tr;
    [SerializeField] AudioClip flute;


    // camera
    [SerializeField] CinemachineVirtualCamera playerVirtualCamera;
    [SerializeField] float levelSizeCamera = 19.78f;


    GameObject lastParent;

    private void Start()
    {
        playerVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        playerVirtualCamera.m_Lens.OrthographicSize = levelSizeCamera;
        rb = GetComponent<Rigidbody2D>();
        platformBullet = GetComponent<PlatformBullet>();
        health = GetComponent<Health>();
        progressionManager = FindObjectOfType<ProgressionManager>();
        animator= GetComponentInChildren<Animator>();
        originalSpeed = speedActuel;
        playerCombat = GetComponent<PlayerCombat>();
    capsuleCollider = GetComponentInChildren<CapsuleCollider2D>();
        boxCollider2D= GetComponentInChildren<BoxCollider2D>();
        boxCollider2D.enabled = false;
        playerInput =GetComponent<PlayerInput>();   
        wallGrab = GetComponent<WallGrabPlayer>();
    }
    private void Update()
    {
        if (wallGrab.wallSlide)
        {
            bool facingDirection;
            facingDirection = isFacingRight;
            if (facingDirection != isFacingRight) { animator.SetBool("WallSlide", false); } else { animator.SetBool("WallSlide", true); }

            
           
        }
        freezeTimer -= Time.deltaTime;
        if (freezeTimer < 0) { isFrozen = false; playerInput.enabled = true ; }
        if (isFrozen) { this.transform.position = frozenPosition; return; }

     

        if (isDead) return; // to remove later
            if (IsGrounded() || OnWall()) { coyoteTimeCounter = coyoteTime; hasDoubleJumped = false; }
            else { coyoteTimeCounter -= Time.deltaTime; }
            if (isDashing) { return; }
            FlipPlayer();
            CheckForDash();
            if (health.playerHealth <= 0)
            {
                isDead = true;
                //Death();
            }
          

        if (isCrouching)
        {
            speedActuel = walkingSpeed;
           
        }
        if(!SomethingAbove() && playerCombat.swordOriginal && !isDashing && !walk && !playerCombat.isGroundAttacking)
        {
            StandUp();
        }
        if (IsGrounded()) { animator.SetBool("Grounded", true); } else { animator.SetBool("Grounded", false); }

       
    }
    public bool isFrozen;
   public float freezeTimer;
    Vector2 frozenPosition;
   public void DisableMovement(float freezeTime)
    {
       
        if (freezeTime >0) { isFrozen = true; }
        freezeTimer = freezeTime;
        playerInput.enabled = false;
        frozenPosition = this.transform.position;
       
    }
  
    // Dash Methods
    private void CheckForDash()
    {
        if (!progressionManager.progression[2]) { return; }
        if (Input.GetKeyDown(KeyCode.C) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    public IEnumerator Dash()
    {
        if(!isFrozen)
        {
            walk = false;
            capsuleCollider.enabled = false;
            boxCollider2D.enabled = true;
            health.canTakeDmg = false;
            animator.SetTrigger("Dodge");
            canDash = false;
            isDashing = true;
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
            tr.emitting = true;
            yield return new WaitForSeconds(dashingTime); tr.emitting = false;
            if (SomethingAbove()) { animator.SetBool("Crouching", true); }
            health.canTakeDmg = true;
            rb.gravityScale = originalGravity;
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);

            canDash = true;
        }
      

    }
    // wasd methods
    public void Walk(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded() && !isFrozen)
        {
            walk = !walk;

            if(walk == true && isMoving)
            {
                speedActuel = walkingSpeed;
                animator.SetBool("isRunning", false);
                animator.SetBool("walk", true);
            }
            if (walk == false && isMoving)
            {
                speedActuel = originalSpeed;
                animator.SetBool("walk", false);
                animator.SetBool("isRunning", true);
            }
            else if (walk == true)
            {
                speedActuel = walkingSpeed;
            }
            else if (walk == false)
            {
                speedActuel = originalSpeed;
            }
        }
        
    
  
        
    }
    private void FlipPlayer()
    {
        rb.velocity = new Vector2(horizontal * speedActuel, rb.velocity.y);
        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();

        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
       
    }
    public void Move(InputAction.CallbackContext context) // while attacking cannot move, but jump should break the lock
    {
     
       if(isFrozen) { return; }
        isMoving = true;
        horizontal = context.ReadValue<Vector2>().x;
        if (context.performed) { transform.SetParent(null); }
        if (lastParent != null && context.canceled && IsGrounded()) { transform.SetParent(lastParent.transform); }


      if(walk == true)
        {
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
            animator.SetBool("isRunning", true);

        }
        if (context.canceled)
        {
            animator.SetBool("walk", false);
            animator.SetBool("isRunning", false);
            isMoving= false;
        }

    }
    // movement methods

  
    public void Jump(InputAction.CallbackContext context) // when jumping, save their Y , if the difference is a large fall, have them play a heavy landing animation
    {
        if (isFrozen) { return; }
        speedActuel = originalSpeed;
        if (context.performed && coyoteTimeCounter > 0 || context.performed && hasDoubleJumped == false || context.performed && allowDoubleWallJump)
        {
            animator.SetBool("Grounded", false);
           
            animator.SetBool("walk", false);
            animator.SetBool("isRunning", false);
            animator.SetTrigger("Jump");

            if (isCrouching) { rb.velocity = new Vector2(rb.velocity.x, jumpingPower / shrunkJumpingPower); }
            else { rb.velocity = new Vector2(rb.velocity.x, jumpingPower); }

            if (coyoteTime > coyoteTimeCounter)
            {
                hasDoubleJumped = !hasDoubleJumped; // THIS IS THE ONLY PLACE THAT SETS HAS DOUBLE JUMPED TO TRUE! NEED TO ADD A TIMER TO ALLOW COYOTE TIMER TO AAPLY

            }

            
        }
        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
            
        }
        
    }
    public bool SomethingAbove()
    {

        return Physics2D.OverlapCircle(roofCheck.transform.position, 0.5f, groundLayer); // needs to be changed to roof layer
        
    }
    
    public bool IsGrounded()
    {

        // if is moving play roll animation in correct direction
        if (!progressionManager.progression[1]) { hasDoubleJumped = true; }
        if (isTouchingBridge) { return true; }
       
        if (isMoving && speedActuel == originalSpeed)
        {
            animator.SetBool("isRunning", true);
        }
        else if (isMoving && speedActuel == walkingSpeed)
        {
            animator.SetBool("walk", true);
        }
        
        return Physics2D.OverlapCircle(groundCheck.transform.position, 0.5f, groundLayer);

    }

    private bool OnWall()
    {
        if (!progressionManager.progression[1]) { hasDoubleJumped = true; } // im not sure this does anything
        return Physics2D.OverlapCircle(groundCheck.transform.position, 0.5f, wallLayer);
    }


    public void PlayFlute(InputAction.CallbackContext context)
    {
        if (!progressionManager.progression[6]) { return; }

        if (fluteIsPlaying) { return; }
        SoundManager.Instance.PlaySound(flute);
        fluteIsPlaying = true;
        Cerberus cerberus = FindObjectOfType<Cerberus>();
        if (cerberus != null)
        {
            StartCoroutine(FindObjectOfType<Cerberus>().SetSleepCerberus());

        }
        StartCoroutine(ResetFlute());

    }
    private IEnumerator ResetFlute()
    {

        yield return new WaitForSeconds(fluteCooldown);

        fluteIsPlaying = false;
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (!progressionManager.progression[0] || !IsGrounded() || isFrozen)  { return; }


        
        isCrouching = true;
        playerCombat.swordOriginal = false;
        if (context.performed)
        {
            capsuleCollider.enabled= false;
            boxCollider2D.enabled= true;
            animator.SetBool("Crouching", true);
           
        }
        playerCombat.swordCrouchPosition = true;
        if (context.canceled && !SomethingAbove())
        {
            
            StandUp();
        }
    }

   
  
   private void StandUp()
    {
        capsuleCollider.enabled = true;
        boxCollider2D.enabled = false;
        playerCombat.swordCrouchPosition = false;  playerCombat.swordOriginal = true;
        animator.SetBool("Crouching", false);
        isCrouching = false;


        speedActuel = originalSpeed;


    }
    void ResetSpeed()
    {
        
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            
            lastParent = collision.gameObject;
            transform.SetParent(collision.transform);           
            

            IsGrounded();
            Vector2 position = this.transform.position;

        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<EnemyHealth>() == null) // this will add fireball hits in cerberus script
            {
                health.TakeDamage(collision.gameObject.GetComponentInParent<EnemyHealth>().contactDamage);
                if (collision.transform.position.x < this.transform.position.x)
                {

                }
                else
                {

                }
            }
            else
            {
                health.TakeDamage(collision.gameObject.GetComponent<EnemyHealth>().contactDamage);
                
            }

            health.CheckIfAlive();
        }
        if (collision.gameObject.CompareTag("Projectile"))
        {
            if (collision.gameObject.GetComponent<EnemyHealth>() == null) 
            {
                if(collision.gameObject.GetComponent<DeflectedFireball>() != null) { return; } // if this is a deflected fireball, then do not applay contact damage
                health.TakeDamage(collision.gameObject.GetComponentInParent<EnemyHealth>().contactDamage);
              
            }
            else
            {
                health.TakeDamage(collision.gameObject.GetComponent<EnemyHealth>().contactDamage);

            }

            health.CheckIfAlive();
        }

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("WeakWall"))// allows walljump
        {
            if (!progressionManager.progression[5]) { return; }
            hasDoubleJumped = false;
            if (progressionManager.progression[10])
            {

                StartCoroutine(AllowDoubleWallJump());

            }

        }

        if (collision.gameObject.CompareTag("Bridge"))
        {
            isTouchingBridge = true;
            StartCoroutine(StopBridge());
        }
        else { isTouchingBridge = false; }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
        
        transform.SetParent(null);

    }
    private IEnumerator AllowDoubleWallJump()
    {
        allowDoubleWallJump = true;

        yield return new WaitForSeconds(2);
        allowDoubleWallJump = false;



    }
    private IEnumerator StopBridge()
    {
        yield return new WaitForSeconds(.1f);

        isTouchingBridge = false;
    }











    bool dodging = false;
    bool wallSlide = false;

    public void ResetDodging()
    {
        dodging = false;
    }

    public void SpawnDustEffect(GameObject dust, float dustXOffset = 0, float dustYOffset = 0)
    {
        if (dust != null)
        {
            // Set dust spawn position
            Vector3 dustSpawnPosition = transform.position + new Vector3(dustXOffset * getFacingDirection(), dustYOffset, 0.0f);
            GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity) as GameObject;
            // Turn dust in correct X direction
            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(getFacingDirection(), 1, 1);
        }
    }
    private float getFacingDirection()
    {
        if(isFacingRight) { return 1; }
        else { return -1; }
    }
    public bool IsWallSliding()
    {
        return wallSlide;
    }
}