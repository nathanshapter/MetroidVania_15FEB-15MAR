using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlatformBulletMovement : MonoBehaviour
{

    [Header("==========Bullet Variables===========")]
    [Space(20)]
    [SerializeField] float bulletSpeed;
    [SerializeField] float leftOffset, rightOffset;
    [Space(20)]

    [Header("Bullet Gets(Automatic)")]
    [Space(20)]
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlatformBullet platformBullet;


    Rigidbody2D rb;
    private Transform otherPosition;

    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        platformBullet = FindObjectOfType<PlatformBullet>();
        GetDirection();

    }

    private void GetDirection()
    {
        if (playerMovement.isFacingRight)
        {
            rb.velocity = new Vector2(bulletSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-bulletSpeed, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) // on collision with platform bullet
    {
        otherPosition = other.transform;
        if (other.transform.CompareTag("Wall") || other.transform.CompareTag("Floor"))
        {
            Instantiate(platformBullet.wallFloor, ReturnPosition(), transform.rotation); 
            
            Destroy(this.gameObject);
        }
        if (other.transform.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
            // cause knockback, but no damage
        }
        if (other.transform.CompareTag("Player"))
        {
            // do nothing
        }
        else 
        { 
            Destroy(this.gameObject);
        }      

        
    }
  private Vector2 ReturnPosition() // gets where the bullet lands to return how to spawn the floor
    {
        if(otherPosition.position.x > this.transform.position.x)
        {
            return this.transform.position + new Vector3(leftOffset,0);
        }
        else
        {
            return this.transform.position + new Vector3(rightOffset, 0);
        }        
    }

}
