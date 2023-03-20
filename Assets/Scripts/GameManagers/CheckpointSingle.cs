using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    CheckpointsManager cpManager;   

    private void Start()
    {
        cpManager = FindObjectOfType<CheckpointsManager>();
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {        
       
        if (other.gameObject.CompareTag("Player")) // sets checkpoint respawn position to this one
        {           
            cpManager.lastCheckPointPos = this.transform;
        }
    }
}
