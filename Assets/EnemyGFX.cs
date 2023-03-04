using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPATH;

    private void Update()
    {
        if(aiPATH.desiredVelocity.x >= .01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(aiPATH.desiredVelocity.x <= 0.01)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
