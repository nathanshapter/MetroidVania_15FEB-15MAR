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
        
        print(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
           
            cpManager.lastCheckPointPos = this.transform;
        }
    }
}
