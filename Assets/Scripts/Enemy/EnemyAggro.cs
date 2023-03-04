using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float aggroRange, aggroAwakeRange;
    [SerializeField] float moveSpeed;

    Rigidbody2D rb;
   [SerializeField] bool awake = false;

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
        else if (distanceToPlayer  +10 > aggroAwakeRange)
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
}
