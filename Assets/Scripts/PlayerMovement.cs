using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] GameObject groundCheck;
    public LayerMask groundLayer;

    [SerializeField] private float horizontal, speed, jumpingPower;
    public bool isFacingRight = true;
    [Range(1f, 2f)] [SerializeField] float shrunkJumpingPower;
    PlatformBullet platformBullet;
    [SerializeField] Transform bulletSpawn;

    bool bulletJustSpawned;
    bool shrunk;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        platformBullet = GetComponent<PlatformBullet>();
    }
    private void Update()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if(!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if(isFacingRight && horizontal< 0f)
        {
            Flip();
            
        }
       
    }
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
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale= transform.localScale;
        localScale.x *= -1f;
        transform.localScale= localScale;
    }
    public void Move(InputAction.CallbackContext context) 
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
    public void Fire(InputAction.CallbackContext context)
    {
        
        if (!bulletJustSpawned)
        {
            Instantiate(platformBullet.bullet, bulletSpawn.position, transform.rotation);
            bulletJustSpawned = true;
            StartCoroutine(ResetBullet());
            
        }
        
        
   }
    IEnumerator ResetBullet()
    {
        yield return new WaitForSeconds(3);
        bulletJustSpawned= false;
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        
        if(context.started)
        {
            transform.DOScaleY(0.5f, 0.2f).SetEase(Ease.InSine);
            shrunk = true;
        }
        if(context.canceled)
        {
            transform.DOScaleY(1, .5f);
            shrunk= false;
        }
       
    }
}
