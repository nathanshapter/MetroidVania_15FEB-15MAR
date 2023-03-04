using Cinemachine;
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
    RespawnManager respawnManager;

    // component Gets
    Rigidbody2D rb;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    [SerializeField] GameObject groundCheck;
    [SerializeField] Transform bulletSpawn;

    //movement values
    [SerializeField] private float horizontal, speed, jumpingPower, platformSpeed, originalMovementSpeed;
    public bool isFacingRight = true;
    [Range(1f, 2f)][SerializeField] float shrunkJumpingPower;
    bool shrunk;

    private bool canDash = true, isDashing;
    [SerializeField] private float dashingPower, dashingTime, dashingCooldown;
    [SerializeField] float coyoteTime = 0.5f, coyoteTimeCounter;
    [SerializeField] bool hasDoubleJumped;

    bool isDead = false;


    // firing values
    bool bulletPlatformJustSpawned;
    [SerializeField] float timeBetweenBullets;




    public bool fluteIsPlaying;
    [SerializeField] float fluteCooldown;


    [SerializeField] TrailRenderer tr;

    [SerializeField] float knockBackX, knockBackY;

    [SerializeField] AudioClip flute;

    bool allowDoubleWallJump = false;

    [SerializeField] CinemachineVirtualCamera playerVirtualCamera;
    [SerializeField] float levelSizeCamera = 19.78f;
    private void Start()
    {
        playerVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        playerVirtualCamera.m_Lens.OrthographicSize = levelSizeCamera;
        rb = GetComponent<Rigidbody2D>();
        platformBullet = GetComponent<PlatformBullet>();
        health = GetComponent<Health>();
        progressionManager = FindObjectOfType<ProgressionManager>();
    }
    private void Update()
    {
        // playerVirtualCamera.m_Lens.OrthographicSize = levelSizeCamera; // this is just for debugging, to be removed
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
        print(IsGrounded());
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
        if (context.performed) { transform.SetParent(null); }
        if (lastParent != null && context.canceled && IsGrounded()) { transform.SetParent(lastParent.transform); }

        

    }
    // movement methods
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && coyoteTimeCounter > 0 || context.performed && hasDoubleJumped == false || context.performed && allowDoubleWallJump)
        {


            if (shrunk) { rb.velocity = new Vector2(rb.velocity.x, jumpingPower / shrunkJumpingPower); }
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
    bool isTouchingBridge;
    private bool IsGrounded()
    {
        if (!progressionManager.progression[1]) { hasDoubleJumped = true; }
        if (isTouchingBridge) { return true; }
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
        if (!progressionManager.progression[0]) { return; }
        if (context.started)
        {
            transform.DOScaleY(0.5f, 0.1f).SetEase(Ease.InSine);
            shrunk = true;
        }
        if (context.canceled)
        {
            transform.DOScaleY(1, .2f);
            shrunk = false;
        }

    }

    // firing methods // this should have its own script but oh well
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
        yield return new WaitForSeconds(timeBetweenBullets);
        bulletPlatformJustSpawned = false;
    }

    GameObject lastParent;
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
                health.TakeDamage(collision.gameObject.GetComponentInParent<EnemyHealth>().attackdamage);
                if (collision.transform.position.x < this.transform.position.x)
                {

                }
                else
                {

                }
            }
            else
            {
                health.TakeDamage(collision.gameObject.GetComponent<EnemyHealth>().attackdamage);
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
}
