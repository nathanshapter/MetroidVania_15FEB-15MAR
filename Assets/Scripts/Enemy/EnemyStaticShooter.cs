using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStaticShooter : MonoBehaviour
{
  [SerializeField]  Transform player;

    [SerializeField] float aggroRange, aggroAwakeRange;
    bool awake = false;

    private void Update()
    {



        

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
      // print(distanceToPlayer);
        if (distanceToPlayer < aggroRange)
        {
            awake = true;
            transform.right = player.position - transform.position;
        }
        else if (distanceToPlayer < aggroAwakeRange && awake == true)
        {
            transform.right = player.position - transform.position;
        }
        else if (distanceToPlayer > aggroAwakeRange)
        {
            awake = false;
            // return to original spot
        }
    }
}
