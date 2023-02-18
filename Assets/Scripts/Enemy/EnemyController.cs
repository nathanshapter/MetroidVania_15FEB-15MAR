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
    [SerializeField]  float baseCastDist;
    [SerializeField]  Transform castPos;

    private bool doesChasePlayer;

    // standard patrol info
    private void Start()
    {
        baseScale= transform.localScale;        
        facingDirection = RIGHT;       
        rb = GetComponent<Rigidbody2D>();       
       
    }
  
    private void FixedUpdate()
    {
        if(doesChasePlayer ) // i think i can remove this 
        {
            return;
        }
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

        float castDist = baseCastDist;
        if(facingDirection == LEFT)
        {
            castDist = -baseCastDist;
        }
        else { castDist= baseCastDist;}

        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;
        Debug.DrawLine(castPos.position, targetPos, Color.blue);
        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Floor")) || Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Player")))
        {
            val = true;
        }
        else { val= false; }
        return val;
    }
    bool isNearEdge()
    {
        bool val = true;
        float castDist = baseCastDist;       
        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;
        Debug.DrawLine(castPos.position, targetPos, Color.red);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Floor")))
        {
            val = false;
        }
        else { val = true; }

        return val;
    }


    

    
}
