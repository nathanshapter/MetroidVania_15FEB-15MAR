using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] GameObject groundCheck;
    public LayerMask groundLayer;

  [SerializeField]  private float horizontal, speed, jumpingPower;
    private bool isFacingRight = true;

    PlatformBullet platformBullet;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
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
        Instantiate(platformBullet.bullet);
    }
}
