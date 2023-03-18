using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectableProjectile : MonoBehaviour
{
   
    private void Start()
    {
       
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<PlayerCombat>() != null)
        {
            if (other.gameObject.GetComponent<PlayerCombat>().isParrying)
            {
              
            }
        }
       
    }
}
