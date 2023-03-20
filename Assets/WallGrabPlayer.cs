using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrabPlayer : MonoBehaviour
{

   [SerializeField] private WallGrabSensor upperRight, upperLeft, lowerRight, lowerLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (upperRight && upperLeft && lowerRight && lowerLeft)
        {
            print(" yes");
        }
    }
}
