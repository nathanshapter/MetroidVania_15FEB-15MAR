using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    // [SerializeField] BoxCollider2D[] scenePoints; // places to change the scene
    // need to add a starting point, and an end point
    [SerializeField] Transform areaIn, areaOut;
    [SerializeField] GameObject player;
    private void Start()
    {
       // scenePoints = GetComponentsInChildren<BoxCollider2D>();
    }
   
   public void movePlayer()
    {
        player.transform.position = areaOut.transform.position;
    }
}
