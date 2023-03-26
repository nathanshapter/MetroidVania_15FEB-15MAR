using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrabPlayer : MonoBehaviour
{
   public bool wallSlide = false;
   [SerializeField] private WallGrabSensor upperRight, upperLeft, lowerRight, lowerLeft;
    PlayerMovement player;
  Animator animator;
    private bool ledgeClimb = false;
    private bool ledgeGrab = false;
    Rigidbody2D rb;
    private Vector3 climbPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();    
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        bool prevWallSlide = wallSlide;
        if (upperRight && upperLeft && lowerRight && lowerLeft)
        {
            wallSlide = (upperRight.State() && lowerRight.State()   && !player.IsGrounded());
            animator.SetBool("WallSlide", wallSlide);
          

        }
    }




}

