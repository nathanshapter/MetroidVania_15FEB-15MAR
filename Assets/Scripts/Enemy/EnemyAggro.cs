using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    [Header("==========Variables To Set==========")]
    [Space(20)]
    [SerializeField] float aggroAwakeRange;
    [SerializeField] float aggroRange;
    [SerializeField] float returnHomeRange;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform player;
    

    Rigidbody2D rb;
    bool awake = false;

    Vector2 startPos;

    
    private void Start()
    {
        startPos= transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ProcessPlayerChase();
    }

    private void ProcessPlayerChase()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < aggroRange)
        {
            awake = true;
            ChasePlayer();
        }
        else if (distanceToPlayer < aggroAwakeRange && awake == true)
        {
            ChasePlayer();
        }
        else if (distanceToPlayer   > returnHomeRange)
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
        if (transform.position.x < player.position.x)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else { rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
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
