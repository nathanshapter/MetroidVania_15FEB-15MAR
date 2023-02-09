using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    //component GETS
    Rigidbody2D rb;
    [SerializeField] GameObject groundCheck;
    public LayerMask groundLayer;
    [SerializeField] Transform bulletSpawn;
    PlatformBullet platformBullet;
    // value SETS
    [SerializeField] private float horizontal, speed, jumpingPower, dashPower;
    public bool isFacingRight = true;
    [Range(1f, 2f)][SerializeField] float shrunkJumpingPower;
    [SerializeField] private float dashingPower, dashingTime, dashingCooldown;
    [SerializeField] TrailRenderer tr;
    bool bulletPlatformJustSpawned, shrunk, canDash = true, isDashing;




    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        platformBullet = GetComponent<PlatformBullet>();
    }
    private void Update()
    {
        if (isDashing) { return; }
        DashBegin();
        FlipPlayer();
    }

    // WASD Methods


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

    /////////////////////////////////////DASH METHODS//////////////////////////////
    private void DashBegin()
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
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime); tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }

    /// ///////////////////Movement Methods//// //////////////////


    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            if(shrunk) { rb.velocity = new Vector2(rb.velocity.x, jumpingPower /shrunkJumpingPower ); }
            else { rb.velocity = new Vector2(rb.velocity.x, jumpingPower ); }
        }
        if(context.canceled && rb.velocity.y> 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
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



    /// //////////////////////////////////////Shooting Methods //////////////////////////////////
   
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




    }            // to implement shooting
}
