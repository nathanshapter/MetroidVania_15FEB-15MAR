using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    // script Gets
    PlatformBullet platformBullet;

    // component Gets
    Rigidbody2D rb;
    public LayerMask groundLayer;
    [SerializeField] GameObject groundCheck;
    [SerializeField] Transform bulletSpawn;

    //movement values
    [SerializeField] private float horizontal, speed, jumpingPower;
    public bool isFacingRight = true;
    [Range(1f, 2f)][SerializeField] float shrunkJumpingPower;
    bool shrunk;
    private bool canDash = true, isDashing;
    [SerializeField] private float dashingPower, dashingTime, dashingCooldown;
    private float coyoteTime = 0.2f, coyoteTimeCounter;

    // firing values
    bool bulletPlatformJustSpawned;
    

   
    

    
 
    [SerializeField] TrailRenderer tr;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        platformBullet = GetComponent<PlatformBullet>();
    }
    private void Update()
    {
        if(IsGrounded()) { coyoteTimeCounter = coyoteTime; }
        else { coyoteTimeCounter -= Time.deltaTime; }
        if (isDashing) { return; }
        FlipPlayer();
        CheckForDash();
    }
    // Dash Methods
    private void CheckForDash()
    {
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
        if(context.performed && coyoteTimeCounter > 0)
        {
            if(shrunk) { rb.velocity = new Vector2(rb.velocity.x, jumpingPower /shrunkJumpingPower ); }
            else { rb.velocity = new Vector2(rb.velocity.x, jumpingPower ); }
        }
        if(context.canceled && rb.velocity.y> 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.transform.position, 0.2f, groundLayer);
    }
   
    
    

    public void Crouch(InputAction.CallbackContext context)
    {
        
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
    public void Fire(InputAction.CallbackContext context)
    {




    }
}
