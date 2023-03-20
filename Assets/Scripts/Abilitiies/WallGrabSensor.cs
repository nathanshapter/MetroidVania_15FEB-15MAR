using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrabSensor : MonoBehaviour
{
    private int colliderCount = 0;
    private float disableTimer;


    void OnTriggerEnter2D(Collider2D other)
    {
        colliderCount++;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        colliderCount--;
    }
    private void Update()
    {
        disableTimer -= Time.deltaTime;
        
    }
    public bool State()
    {
        if (disableTimer > 0)
            return false;
        return colliderCount > 0;
    }
    public void Disable(float duration)
    {
        disableTimer = duration;
    }
}
