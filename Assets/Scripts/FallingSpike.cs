using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);


        if (collision.gameObject.GetComponent<PlatformScript>().enabled == true)
        {
            print("floor crumbled");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

           
        
       
    }
}
