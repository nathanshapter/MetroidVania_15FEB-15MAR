using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    PlayerMovement player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();    
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        print("inRange");
        if(other.gameObject.CompareTag("Player") && player.lookingToInteract)
        {
           
        }
    }

    private void Update()
    {
        if (player.lookingToInteract) // and in range
        {
            print("interact");
        }
    }
}
