using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerberusFireProjectile : MonoBehaviour
{
  [SerializeField]  float moveSpeed;
    [SerializeField] Health target; // to target player
    Rigidbody2D rb;
    Vector2 moveDirection;

  [SerializeField]  Cerberus cerberus;
    private void Start()
    {
        target = FindObjectOfType<Health>();
        rb= GetComponent<Rigidbody2D>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        cerberus = FindObjectOfType<Cerberus>() ;
    }

    private void OnCollisionEnter2D(Collision2D collision) // player detects the damage, do not need to here
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cerberus.fireballHits++;
        }
        Destroy(gameObject);
    }
}
