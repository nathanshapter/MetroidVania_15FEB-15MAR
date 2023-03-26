using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhastController : MonoBehaviour
{
    Animator animator;
    [Header("==========Variables To Set==========")]
    [Space(20)]
    [SerializeField] float aggroAwakeRange;
    [SerializeField] float aggroRange;
    [SerializeField] float returnHomeRange;
    [SerializeField] float moveSpeed;
    [SerializeField] PlayerMovement player;

    Rigidbody2D rb;
    bool awake = false;

    Vector2 startPos;
    private void Start()
    {
        animator= GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
       startPos= transform.position;
    }

    private void Update()
    {
        ProcessPlayerChase();
    }

    private void ProcessPlayerChase()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < aggroRange)
        {
            awake = true;
            ChasePlayer();
        }
        else if (distanceToPlayer < aggroAwakeRange && awake == true)
        {
            ChasePlayer();
        }
        else if (distanceToPlayer > returnHomeRange)
        {
            awake = false;
            StopChasingPlayer();
        }
    }

    private void StopChasingPlayer()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPos;
    }

    private void ChasePlayer()
    {
        float xSize = transform.localScale.x;
        float ySize = transform.localScale.y;
        if (transform.position.x < player.transform.position.x)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.localScale = new Vector2(xSize, ySize);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.localScale = new Vector2(-xSize, ySize);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroAwakeRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, returnHomeRange);
    }
}
