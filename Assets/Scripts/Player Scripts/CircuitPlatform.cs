using System.Collections;
using UnityEngine;



public class CircuitPlatform : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    Vector2 startPosition;
    [SerializeField] float movementSpeed = 10f;
    float startingSpeed;
    [SerializeField] float dockTimer;



    [SerializeField] float dockStoppingDistance = 1f;
    [SerializeField] float dockDecelerationSpeed = 10f;
    [SerializeField] float dockAccelerationSpeed = 5f;


    
    bool delay;
    [SerializeField] float delayTime;

    [SerializeField] bool reverseDirection;
    /// <summary>
    /// ////////////////////////////////// ADD AN OPTION TO REVERSE THE PATH
    /// </summary>
   
    
    private void Start()
    {
        startPosition= transform.position;
        startingSpeed = movementSpeed;

       
        
        StartCoroutine(PausePlatform(delayTime));

        if (reverseDirection)
        {
            System.Array.Reverse(waypoints);
        }
    }
    int index = 0;
    private void Update()
    {
        
        if (delay) { return; }
      
        if (index >= waypoints.Length)
        {
            index =0;
        }
        else
        {
            Vector2 newPos = Vector2.MoveTowards(transform.position, waypoints[index].transform.position, movementSpeed * Time.deltaTime);
            transform.position = newPos;
        }


     float distance =  Vector2.Distance(waypoints[index].position, transform.position);


     

        if (distance <= dockStoppingDistance )
        {
            if(dockTimer >= dockStoppingDistance)
            {
                movementSpeed = distance *dockDecelerationSpeed;
                

            }
            if(!coroutineStarted)
                StartCoroutine(IncreaseIndex());


        }
        else if(distance > dockStoppingDistance)
        {
            movementSpeed += dockAccelerationSpeed * Time.deltaTime;

            if(movementSpeed >= startingSpeed)
            {
                movementSpeed = startingSpeed;
            }
        }
        
      
    }
    int gizmoNumber = 0;
    bool coroutineStarted = false;
    IEnumerator IncreaseIndex()
    {
        
        
            coroutineStarted = true;
            yield return new WaitForSeconds(dockTimer);
            index++;
            coroutineStarted = false;
        
      
    }

    IEnumerator PausePlatform(float delayTime)
    {
        delay = true;
        yield return new WaitForSeconds(delayTime);
        delay = false;
    }


    private void OnDrawGizmos()
    {
        if(index == 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, waypoints[0].transform.position);
        }
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(waypoints[waypoints.Length-1].transform.position, waypoints[0].transform.position);

        foreach (var item in waypoints)
        {
            
            if (gizmoNumber +1 >= waypoints.Length) 
            { 

                gizmoNumber = 0;
              
            }
            Gizmos.DrawLine(waypoints[gizmoNumber].position, waypoints[gizmoNumber+1].transform.position);
            gizmoNumber++;
        }

       
    }

  
}
