using UnityEngine;
using System.Collections;

public class PrototypeHero : MonoBehaviour {

    public float      runSpeed = 4.5f;
    public float      walkSpeed = 2.0f;
    public float      jumpForce = 7.5f;
    public float      dodgeForce = 8.0f;
    public float      parryKnockbackForce = 4.0f; 
    public bool       noBlood = false;
    public bool       hideSword = false;

    private Animator            animator;
    private Rigidbody2D         body2d;
    private SpriteRenderer      SR;
    private Sensor_Prototype    groundSensor;
    private Sensor_Prototype    wallSensorR1;
    private Sensor_Prototype    wallSensorR2;
    private Sensor_Prototype    wallSensorL1;
    private Sensor_Prototype    wallSensorL2;
    private bool                grounded = false;
    private bool                moving = false;
    private bool                dead = false;
    private bool                dodging = false;
    private bool                wallSlide = false;
    private bool                ledgeGrab = false;
    private bool                ledgeClimb = false;
    private bool                crouching = false;
    private Vector3             climbPosition;
    private int                 facingDirection = 1;
    private float               disableMovementTimer = 0.0f;
    private float               parryTimer = 0.0f;
    private float               respawnTimer = 0.0f;
    private Vector3             respawnPosition = Vector3.zero;
    private int                 currentAttack = 0;
    private float               timeSinceAttack = 0.0f;
    private float               gravity;
    public float                maxSpeed = 4.5f;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponentInChildren<Animator>();
        body2d = GetComponent<Rigidbody2D>();
        SR = GetComponentInChildren<SpriteRenderer>();
        gravity = body2d.gravityScale;

        groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Prototype>();
        wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_Prototype>();
        wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_Prototype>();
        wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_Prototype>();
        wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_Prototype>();
    }

    // Update is called once per frame
    void Update ()
    {
        
        // Decrease death respawn timer 
        respawnTimer -= Time.deltaTime;

        // Increase timer that controls attack combo
        timeSinceAttack += Time.deltaTime;

        // Decrease timer that checks if we are in parry stance
        parryTimer -= Time.deltaTime;

        // Decrease timer that disables input movement. Used when attacking
        disableMovementTimer -= Time.deltaTime;

        // Respawn Hero if dead
        if (dead && respawnTimer < 0.0f)
            RespawnHero();

        if (dead)
            return;

        //Check if character just landed on the ground
        if (!grounded && groundSensor.State())
        {
            grounded = true;
            animator.SetBool("Grounded", grounded);
        }

        //Check if character just started falling
        if (grounded && !groundSensor.State())
        {
            grounded = false;
            animator.SetBool("Grounded", grounded);
        }

        // -- Handle input and movement --
        float inputX = 0.0f;

        if (disableMovementTimer < 0.0f)
            inputX = Input.GetAxis("Horizontal");
        

        // GetAxisRaw returns either -1, 0 or 1
        float inputRaw = Input.GetAxisRaw("Horizontal");

        // Check if character is currently moving
        if (Mathf.Abs(inputRaw) > Mathf.Epsilon && Mathf.Sign(inputRaw) == facingDirection)
            moving = true;
        else
            moving = false;

        // Swap direction of sprite depending on move direction
        if (inputRaw > 0 && !dodging && !wallSlide && !ledgeGrab && !ledgeClimb)
        {
            SR.flipX = false;
            facingDirection = 1;
        }
            
        else if (inputRaw < 0 && !dodging && !wallSlide && !ledgeGrab && !ledgeClimb)
        {
            SR.flipX = true;
            facingDirection = -1;
        }
     
        // SlowDownSpeed helps decelerate the characters when stopping
        float SlowDownSpeed = moving ? 1.0f : 0.5f;
        // Set movement
        if(!dodging && !ledgeGrab && !ledgeClimb && !crouching && parryTimer < 0.0f)
            body2d.velocity = new Vector2(inputX * maxSpeed * SlowDownSpeed, body2d.velocity.y);

        // Set AirSpeed in animator
        animator.SetFloat("AirSpeedY", body2d.velocity.y);

        // Set Animation layer for hiding sword
        int boolInt = hideSword ? 1 : 0;
        animator.SetLayerWeight(1, boolInt);

        // Check if all sensors are setup properly
        if (wallSensorR1 && wallSensorR2 && wallSensorL1 && wallSensorL2)
        {
            bool prevWallSlide = wallSlide;
            //Wall Slide
            // True if either both right sensors are colliding and character is facing right
            // OR if both left sensors are colliding and character is facing left
            wallSlide = (wallSensorR1.State() && wallSensorR2.State() && facingDirection == 1) || (wallSensorL1.State() && wallSensorL2.State() && facingDirection == -1);
            if (grounded)
                wallSlide = false;
            animator.SetBool("WallSlide", wallSlide);
            //Play wall slide sound
            if(prevWallSlide && !wallSlide)
                AudioManager_PrototypeHero.instance.StopSound("WallSlide");


            //Grab Ledge
            // True if either bottom right sensor is colliding and top right sensor is not colliding 
            // OR if bottom left sensor is colliding and top left sensor is not colliding 
            bool shouldGrab = !ledgeClimb && !ledgeGrab && ((wallSensorR1.State() && !wallSensorR2.State()) || (wallSensorL1.State() && !wallSensorL2.State()));
            if(shouldGrab)
            {
                Vector3 rayStart;
                if (facingDirection == 1)
                    rayStart = wallSensorR2.transform.position + new Vector3(0.2f, 0.0f, 0.0f);
                else
                    rayStart = wallSensorL2.transform.position - new Vector3(0.2f, 0.0f, 0.0f);

                var hit = Physics2D.Raycast(rayStart, Vector2.down, 1.0f);

                GrabableLedge ledge = null;
                if(hit)
                    ledge = hit.transform.GetComponent<GrabableLedge>();

                if (ledge)
                {
                    ledgeGrab = true;
                    body2d.velocity = Vector2.zero;
                    body2d.gravityScale = 0;
                    
                    climbPosition = ledge.transform.position + new Vector3(ledge.topClimbPosition.x, ledge.topClimbPosition.y, 0);
                    if (facingDirection == 1)
                        transform.position = ledge.transform.position + new Vector3(ledge.leftGrabPosition.x, ledge.leftGrabPosition.y, 0);
                    else
                        transform.position = ledge.transform.position + new Vector3(ledge.rightGrabPosition.x, ledge.rightGrabPosition.y, 0);
                }
                animator.SetBool("LedgeGrab", ledgeGrab);
            }
            
        }


        // -- Handle Animations --
        //Death
        if (Input.GetKeyDown("e") && !dodging)
        {
            animator.SetBool("noBlood", noBlood);
            animator.SetTrigger("Death");
            respawnTimer = 2.5f;
            DisableWallSensors();
            dead = true;
        }
        
        //Hurt
        else if (Input.GetKeyDown("q") && !dodging)
        {
            animator.SetTrigger("Hurt");
            // Disable movement 
            disableMovementTimer = 0.1f;
            DisableWallSensors();
        }

        // Parry & parry stance
        else if (Input.GetMouseButtonDown(1) && !dodging && !ledgeGrab && !ledgeClimb && !crouching && grounded)
        {
            // Parry
            // Used when you are in parry stance and something hits you
            if (parryTimer > 0.0f)
            {
                animator.SetTrigger("Parry");
                body2d.velocity = new Vector2(-facingDirection * parryKnockbackForce, body2d.velocity.y);
            }
                
            // Parry Stance
            // Ready to parry in case something hits you
            else
            {
                animator.SetTrigger("ParryStance");
                parryTimer = 7.0f / 12.0f;
            }
        }

        //Up Attack
        else if (Input.GetMouseButtonDown(0) && Input.GetKey("w") && !dodging && !ledgeGrab && !ledgeClimb && !crouching && grounded && timeSinceAttack > 0.2f)
        {
            animator.SetTrigger("UpAttack");

            // Reset timer
            timeSinceAttack = 0.0f;

            // Disable movement 
            disableMovementTimer = 0.35f;
        }

        //Attack
        else if (Input.GetMouseButtonDown(0) && !dodging && !ledgeGrab && !ledgeClimb && !crouching && grounded && timeSinceAttack > 0.2f)
        {
            // Reset timer
            timeSinceAttack = 0.0f;

            currentAttack++;

            // Loop back to one after second attack
            if (currentAttack > 2)
                currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (timeSinceAttack > 1.0f)
                currentAttack = 1;

            // Call one of the two attack animations "Attack1" or "Attack2"
            animator.SetTrigger("Attack" + currentAttack);

            // Disable movement 
            disableMovementTimer = 0.35f;
        }

        //Air Slam Attack
        else if (Input.GetMouseButtonDown(0) && Input.GetKey("s") && !ledgeGrab && !ledgeClimb && !grounded)
        {
            animator.SetTrigger("AttackAirSlam");
            body2d.velocity = new Vector2(0.0f, -jumpForce);
            disableMovementTimer = 0.8f;

            // Reset timer
            timeSinceAttack = 0.0f;
        }

        // Air Attack Up
        else if (Input.GetMouseButtonDown(0) && Input.GetKey("w") && !dodging && !ledgeGrab && !ledgeClimb && !crouching && !grounded && timeSinceAttack > 0.2f)
        {
            Debug.Log("Air attack up");
            animator.SetTrigger("AirAttackUp");

            // Reset timer
            timeSinceAttack = 0.0f;
        }

        // Air Attack
        else if (Input.GetMouseButtonDown(0) && !dodging && !ledgeGrab && !ledgeClimb && !crouching && !grounded && timeSinceAttack > 0.2f)
        {
            animator.SetTrigger("AirAttack");

            // Reset timer
            timeSinceAttack = 0.0f;
        }

        // Dodge
        else if (Input.GetKeyDown("left shift") && grounded && !dodging && !ledgeGrab && !ledgeClimb)
        {
            dodging = true;
            crouching = false;
            animator.SetBool("Crouching", false);
            animator.SetTrigger("Dodge");
            body2d.velocity = new Vector2(facingDirection * dodgeForce, body2d.velocity.y);
        }

        // Throw
        else if(Input.GetKeyDown("f") && grounded && !dodging && !ledgeGrab && !ledgeClimb)
        {
            animator.SetTrigger("Throw");

            // Disable movement 
            disableMovementTimer = 0.20f;
        }

        // Ledge Climb
        else if(Input.GetKeyDown("w") && ledgeGrab)
        {
            DisableWallSensors();
            ledgeClimb = true;
            body2d.gravityScale = 0;
            disableMovementTimer = 6.0f/14.0f;
            animator.SetTrigger("LedgeClimb");
        }

        // Ledge Drop
        else if (Input.GetKeyDown("s") && ledgeGrab)
        {
            DisableWallSensors();
        }

        //Jump
        else if (Input.GetButtonDown("Jump") && (grounded || wallSlide) && !dodging && !ledgeGrab && !ledgeClimb && !crouching && disableMovementTimer < 0.0f)
        {
            // Check if it's a normal jump or a wall jump
            if(!wallSlide)
                body2d.velocity = new Vector2(body2d.velocity.x, jumpForce);
            else
            {
                body2d.velocity = new Vector2(-facingDirection * jumpForce / 2.0f, jumpForce);
                facingDirection = -facingDirection;
                SR.flipX = !SR.flipX;
            }

            animator.SetTrigger("Jump");
            grounded = false;
            animator.SetBool("Grounded", grounded);
            groundSensor.Disable(0.2f);
        }

        //Crouch / Stand up
        else if (Input.GetKeyDown("s") && grounded && !dodging && !ledgeGrab && !ledgeClimb && parryTimer < 0.0f)
        {
            crouching = true;
            animator.SetBool("Crouching", true);
            body2d.velocity = new Vector2(body2d.velocity.x / 2.0f, body2d.velocity.y);
        }
        else if (Input.GetKeyUp("s") && crouching)
        {
            crouching = false;
            animator.SetBool("Crouching", false);
        }
        //Walk
        else if (moving && Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetInteger("AnimState", 2);
            maxSpeed = walkSpeed;
        }

        //Run
        else if(moving)
        {
            animator.SetInteger("AnimState", 1);
            maxSpeed = runSpeed;
        }

        //Idle
        else
            animator.SetInteger("AnimState", 0);
    }

    // Function used to spawn a dust effect
    // All dust effects spawns on the floor
    // dustXoffset controls how far from the player the effects spawns.
    // Default dustXoffset is zero
    public void SpawnDustEffect(GameObject dust, float dustXOffset = 0, float dustYOffset = 0)
    {
        if (dust != null)
        {
            // Set dust spawn position
            Vector3 dustSpawnPosition = transform.position + new Vector3(dustXOffset * facingDirection, dustYOffset, 0.0f);
            GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity) as GameObject;
            // Turn dust in correct X direction
            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(facingDirection, 1, 1);
        }
    }

    void DisableWallSensors()
    {
        ledgeGrab = false;
        wallSlide = false;
        ledgeClimb = false;
        wallSensorR1.Disable(0.8f);
        wallSensorR2.Disable(0.8f);
        wallSensorL1.Disable(0.8f);
        wallSensorL2.Disable(0.8f);
        body2d.gravityScale = gravity;
        animator.SetBool("WallSlide", wallSlide);
        animator.SetBool("LedgeGrab", ledgeGrab);
    }

    // Called in AE_resetDodge in PrototypeHeroAnimEvents
    public void ResetDodging()
    {
        dodging = false;
    }

    public void SetPositionToClimbPosition()
    {
        transform.position = climbPosition;
        body2d.gravityScale = gravity;
        wallSensorR1.Disable(3.0f / 14.0f);
        wallSensorR2.Disable(3.0f / 14.0f);
        wallSensorL1.Disable(3.0f / 14.0f);
        wallSensorL2.Disable(3.0f / 14.0f);
        ledgeGrab = false;
        ledgeClimb = false;
    }

    public bool IsWallSliding()
    {
        return wallSlide;
    }

    public void DisableMovement(float time = 0.0f)
    {
        disableMovementTimer = time;
    }

    void RespawnHero()
    {
        transform.position = Vector3.zero;
        dead = false;
        animator.Rebind();
    }
}
