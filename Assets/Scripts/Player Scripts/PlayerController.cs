using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 4.5f;
    public float walkSpeed = 2.0f;
    public float jumpForce = 7.5f;
    public float dodgeForce = 8.0f;
    public float parryKnockbackForce = 4.0f;
    public bool noBlood = false;
    public bool hideSword = false;

    private Animator animator;
    private Rigidbody2D body2d;
    private SpriteRenderer SR;
    private Sensor_Prototype groundSensor;
    private Sensor_Prototype wallSensorR1;
    private Sensor_Prototype wallSensorR2;
    private Sensor_Prototype wallSensorL1;
    private Sensor_Prototype wallSensorL2;
    private bool grounded = false;
    private bool moving = false;
    private bool dead = false;
    private bool dodging = false;
    private bool wallSlide = false;
    private bool ledgeGrab = false;
    private bool ledgeClimb = false;
    private bool crouching = false;
    private Vector3 climbPosition;
    private int facingDirection = 1;
    private float disableMovementTimer = 0.0f;
    private float parryTimer = 0.0f;
    private float respawnTimer = 0.0f;
    private Vector3 respawnPosition = Vector3.zero;
    private int currentAttack = 0;
    private float timeSinceAttack = 0.0f;
    private float gravity;
    public float maxSpeed = 4.5f;

    private float horizontal;

    // Use this for initialization
    void Start()
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
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
      //  if (context.performed) { transform.SetParent(null); }
       // if (lastParent != null && context.canceled && IsGrounded()) { transform.SetParent(lastParent.transform); }



    }
}
