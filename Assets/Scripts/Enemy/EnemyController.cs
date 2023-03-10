using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{ 

    Rigidbody2D rb;
    float moveSpeed = 5;
    const string LEFT = "left";
    const string RIGHT = "right";
    string facingDirection;
    Vector3 baseScale;
    float wallDistance;
    [SerializeField]  Transform castPosition;

    

    // standard patrol info
    private void Start()
    {
        baseScale= transform.localScale;        
        facingDirection = RIGHT;       
        rb = GetComponent<Rigidbody2D>();       
       
    }
  
    private void FixedUpdate()
    {
        
        float vX = moveSpeed;
        if(facingDirection== LEFT)
        {
            vX = -moveSpeed;
        }

        rb.velocity = new Vector2(vX, rb.velocity.y);
        if (isHittingWall() || isNearEdge())
        {
            if(facingDirection == LEFT)
            {
                ChangeFacingDirection(RIGHT);
            }
            else if(facingDirection == RIGHT)
            {
                ChangeFacingDirection(LEFT);
            }           
        }        
    }

    void ChangeFacingDirection(string newDirection)
    {
        Vector3 newScale = baseScale;
        if(newDirection == LEFT)
        {
            newScale.x = -baseScale.x;
        }
        else { newScale.x = baseScale.x; }
        transform.localScale= newScale;
        facingDirection= newDirection;
    }
    bool isHittingWall()
    {
        bool val = false;

        float castDist = wallDistance;
        if(facingDirection == LEFT)
        {
            castDist = -wallDistance;
        }
        else { castDist= wallDistance;}

        Vector3 targetPos = castPosition.position;
        targetPos.x += castDist;
        Debug.DrawLine(castPosition.position, targetPos, Color.blue);
        if (Physics2D.Linecast(castPosition.position, targetPos, 1 << LayerMask.NameToLayer("Floor")) || Physics2D.Linecast(castPosition.position, targetPos, 1 << LayerMask.NameToLayer("Player")))
        {
            val = true;
        }
        else { val= false; }
        return val;
    }
    bool isNearEdge()
    {
        bool val = true;
        float castDist = wallDistance;       
        Vector3 targetPos = castPosition.position;
        targetPos.y -= castDist;
        Debug.DrawLine(castPosition.position, targetPos, Color.red);

        if (Physics2D.Linecast(castPosition.position, targetPos, 1 << LayerMask.NameToLayer("Floor")))
        {
            val = false;
        }
        else { val = true; }

        return val;
    }


    

    
}
