using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalGroupPlatform : MonoBehaviour
{
    [Header("Straight Movement")]
    Vector2 startingPosition;
    [HideInInspector] public float movementSpeed = 5;
    Transform topYPosition, bottomYPosition;
    PlayerMovement player;

  [SerializeField]  bool isFlipped;
    private void Start()
    {
        player = FindObjectOfType <PlayerMovement>();   
        startingPosition = GetComponentInParent<VerticalPlatformManager>().transform.position;
        movementSpeed = GetComponentInParent<VerticalPlatformManager>().yValue;
        topYPosition = GetComponentInParent<VerticalPlatformManager>().topYPosition;
        bottomYPosition = GetComponentInParent<VerticalPlatformManager>().bottomYPosition;
       isFlipped = GetComponentInParent<VerticalPlatformManager>().isFlipped;
    }
    private void Update()
    {
       
       transform.position += new Vector3(0,-movementSpeed,0) * Time.deltaTime;

       
        if(transform.position.y < bottomYPosition.position.y && !isFlipped)
        {
            player.transform.SetParent(null);
            transform.position = topYPosition.position;
        }

        if (transform.position.y > topYPosition.position.y && isFlipped)
        {
            player.transform.SetParent(null);

            transform.position = bottomYPosition.position;
        }



    }

}
