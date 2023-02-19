using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    // [SerializeField] BoxCollider2D[] scenePoints; // places to change the scene
    // need to add a starting point, and an end point
   
    [SerializeField] GameObject player;
    [SerializeField] GameObject area;
    private void Start()
    {
       // scenePoints = GetComponentsInChildren<BoxCollider2D>();
    }
   
   public void movePlayer(Transform placeToMoveTo)
    {

         player.transform.position = placeToMoveTo.transform.position;
        area.gameObject.SetActive(false);
    
    }
   
}
