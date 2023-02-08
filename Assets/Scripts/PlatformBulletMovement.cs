using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBulletMovement : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
  
    Rigidbody2D rb;
   [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlatformBullet platformBullet;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();       
     playerMovement = FindObjectOfType<PlayerMovement>();
        platformBullet= FindObjectOfType<PlatformBullet>();
        if (playerMovement.isFacingRight)
        {
            rb.velocity = new Vector2(bulletSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-bulletSpeed, rb.velocity.y);
        }
     
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            Instantiate(platformBullet.wallFloor, this.transform.position, transform.rotation);
            
            
            Destroy(this.gameObject);
        }
        
    }

}
