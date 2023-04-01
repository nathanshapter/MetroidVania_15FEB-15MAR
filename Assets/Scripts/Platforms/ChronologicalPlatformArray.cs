using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronologicalPlatformArray : MonoBehaviour
{
    public GameObject[] platform;


    int gizmoNumber = 0;
    private void Start()
    {
        
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;

        foreach (var item in platform)
        {
            if (gizmoNumber + 1 >= platform.Length)
            {
                gizmoNumber = 0;
            }
            Gizmos.DrawLine(platform[gizmoNumber].transform.position, platform[gizmoNumber + 1].transform.position);
            gizmoNumber++;

        }
    }
}
