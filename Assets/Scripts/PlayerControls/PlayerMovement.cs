using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    // script Gets
    PlatformBullet platformBullet;
    Health health;
    ProgressionManager progressionManager;

    // component Gets
    Rigidbody2D rb;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    [SerializeField] GameObject groundCheck;
    [SerializeField] Transform bulletSpawn;

    //movement values
    [SerializeField] private float horizontal, speed, jumpingPower;
    public bool isFacingRight = true;
    [Range(1f, 2f)][SerializeField] float shrunkJumpingPower;
    bool shrunk;
    
    private bool canDash = true, isDashing;
    [SerializeField] private float dashingPower, dashingTime, dashingCooldown;
    private float coyoteTime = 0.25f, coyoteTimeCounter;
  [SerializeField]  bool hasDoubleJumped;
  
    bool isDead = false;
    

    // firing values
    bool bulletPlatformJustSpawned;
    

   
    

    
 
    [SerializeField] TrailRenderer tr;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        platformBullet = GetComponent<PlatformBullet>();
        health = GetComponent<Health>();
        progressionManager= FindObjectOfType<ProgressionManager>();
    }
    private void Update()
    {
        if (isDead) return; // to remove later
        if(IsGrounded() || OnWall()) { coyoteTimeCounter = coyoteTime; hasDoubleJumped = false; }
        else { coyoteTimeCounter -= Time.deltaTime; }
        if (isDashing) { return; }
        FlipPlayer();
        CheckForDash();
       if(health.playerHealth <= 0)
        {
            isDead= true;
            //Death();
        }
    }
    // Dash Methods
    private void CheckForDash()
    {
        if (!progressionManager.progression[2]) { return; }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower , 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime); tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }
    // wasd methods

    private void FlipPlayer()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
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
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
    // movement methods
    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && coyoteTimeCounter > 0 || context.performed && hasDoubleJumped == false)
        {
            
            if(shrunk) { rb.velocity = new Vector2(rb.velocity.x, jumpingPower /shrunkJumpingPower ); }
            else { rb.velocity = new Vector2(rb.velocity.x, jumpingPower ); }
            
            hasDoubleJumped = !hasDoubleJumped;
            
        
        }
        if(context.canceled && rb.velocity.y> 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
    }

    private bool IsGrounded()
    {
        if (!progressionManager.progression[1]) { hasDoubleJumped = true; }
        return Physics2D.OverlapCircle(groundCheck.transform.position, 0.2f, groundLayer);
    }

    private bool OnWall()
    {
        if (!progressionManager.progression[1]) { hasDoubleJumped = true; }
        return Physics2D.OverlapCircle(groundCheck.transform.position, 0.2f, wallLayer);
    }
   
    
    

    public void Crouch(InputAction.CallbackContext context)
    {
        if (!progressionManager.progression[0]) { return; }
        if(context.started)
        {
            transform.DOScaleY(0.5f, 0.1f).SetEase(Ease.InSine);
            shrunk = true;
        }
        if(context.canceled)
        {
            transform.DOScaleY(1, .2f);
            shrunk= false;
        }
       
    }

    // firing methods
    public void FirePlatform(InputAction.CallbackContext context)
    {
        if (!progressionManager.progression[3]) { return; }
        if (!bulletPlatformJustSpawned)
        {
            Instantiate(platformBullet.bullet, bulletSpawn.position, transform.rotation);
            bulletPlatformJustSpawned = true;
            StartCoroutine(ResetPlatformBullet());

        }
    }
    IEnumerator ResetPlatformBullet()
    {
        yield return new WaitForSeconds(3);
        bulletPlatformJustSpawned = false;
    }


    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Vector2 position = this.transform.position;
            if(position.y +.2 < collision.transform.position.y) // .5 is offset so slapping it from side does not destroy it
            {
                Destroy(collision.gameObject);
               
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health.TakeDamage(collision.gameObject.GetComponent<EnemyHealth>().attackdamage);
            health.CheckIfAlive();
        }
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("WeakWall"))// allows walljump
        {
            if (!progressionManager.progression[4]) { return; }
             hasDoubleJumped = false; 
           
        }
    }
}
