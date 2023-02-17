using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
   [SerializeField] Transform player;

   [SerializeField] float aggroRange;
    [SerializeField] float moveSpeed;
    Rigidbody2D rb;

    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        print(distanceToPlayer);

        if(distanceToPlayer < aggroRange)
        {

            ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }
    }

    private void StopChasingPlayer()
    {
       
    }

    private void ChasePlayer()
    {
       
    }
}
