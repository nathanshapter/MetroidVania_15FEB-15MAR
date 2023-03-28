using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    PlayerMovement player;
  public  bool leverPressed = false;
   [SerializeField] GameObject door, topYPosition;
    Vector3 startPosition;
 
  
    [SerializeField] float movementSpeed = 5;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        topYPosition.GetComponent<Renderer>().enabled = false;
        startPosition = door.transform.position;
    }

    private void Update()
    {
        if (leverPressed == true  )
        {
            if( door.transform.position.y  >= topYPosition.transform.position.y) { return; }
            door.transform.position += new Vector3(0, movementSpeed, 0) * Time.deltaTime;
        }
        else
        {
            if(door.transform.position.y <= startPosition.y) { return; }
            door.transform.position -= new Vector3(0, movementSpeed, 0) * Time.deltaTime;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(topYPosition.transform.position, door.transform.position);
    }

}
