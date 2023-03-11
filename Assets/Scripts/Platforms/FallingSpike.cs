using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);


        if(collision.gameObject.GetComponent<PlatformScript>() != null)
        {
           
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

     

           
        
       
    }
}
