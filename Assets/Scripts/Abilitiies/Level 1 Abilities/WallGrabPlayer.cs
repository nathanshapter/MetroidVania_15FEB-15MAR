using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrabPlayer : MonoBehaviour
{
   public bool wallSlide = false;
   [SerializeField] private WallGrabSensor upperRight, upperLeft, lowerRight, lowerLeft;
    PlayerMovement player;
  Animator animator;
  
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();    
 
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

