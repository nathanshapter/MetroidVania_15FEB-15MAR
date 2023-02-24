using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] Transform leftEdge, rightEdge;


    [Header("Enemy")]
    [SerializeField] Transform enemy;
    [SerializeField] float moveSpeed;
    private Vector3 initScale;
    private bool movingLeft;

   [SerializeField] private Animator anim;
    [SerializeField] float idleDuration;
    private float idleTimer;

    private void Awake()
    {
        initScale= enemy.localScale;
    }
    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }
    private void MoveInDirection(int direction)
    {
        idleTimer=0;
        anim.SetBool("moving", true);
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) *direction, initScale.y, initScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * moveSpeed, enemy.position.y, enemy.position.z);
        // make enemy face direction, move there
    }
    private void Update()
    {
        if(movingLeft)
        {
            if(enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
            
        }
        else
        {if(enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
            
        }
       
    }
    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;
        if(idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
        
    }
}
