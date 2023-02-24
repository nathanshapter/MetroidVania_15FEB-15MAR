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
        moveDirection = (new Vector3(target.transform.position.x * Random.Range(1,2.5f), target.transform.position.y * Random.Range(1, 2.5f))  - transform.position).normalized * moveSpeed;
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
