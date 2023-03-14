using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularFireballScript : MonoBehaviour
{
    [Header("============Variables To Set ==========")]
    [SerializeField] float moveSpeed;
     Health target; // to target player
    Rigidbody2D rb;
    Vector2 moveDirection;
    [SerializeField] private AudioClip fireBall;


    private void Start()
    {
       
        // add sound to fireball
        target = FindObjectOfType<Health>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        moveDirection = (new Vector3(target.transform.position.x , target.transform.position.y, 0) - transform.position).normalized *moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
