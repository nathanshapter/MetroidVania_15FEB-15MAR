using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class VerticalPlatformManager : MonoBehaviour
{
    [SerializeField] GameObject platform;
    [SerializeField] List<GameObject> verticalPlatform;
    [SerializeField] int amountOfPlatformsToSpawn; // this needs to be the same for all of them in the scene or does not work correctly
    public Transform topYPosition, bottomYPosition;
    float yDistanceBetweenTopBottom;

    public float yValue;

    public bool isFlipped;


    private void Start() 
    {
        yDistanceBetweenTopBottom = topYPosition.transform.position.y - bottomYPosition.transform.position.y;

        float platformSpawnGap = yDistanceBetweenTopBottom / amountOfPlatformsToSpawn;

        if (!isFlipped)
        {
            for (int i = 0; i < amountOfPlatformsToSpawn; i++)
            {
                Instantiate(platform, ReturnSpawnPosition(), false);

                verticalPlatform.Add(platform);

              
                platform.transform.position = new Vector2(0, platformSpawnGap * i);

            }
        }
        if (isFlipped)
        {
            for (int i = 0; i < amountOfPlatformsToSpawn; i++)
            {
                Instantiate(platform, bottomYPosition, false);

                verticalPlatform.Add(platform);


                platform.transform.position = new Vector2(0, platformSpawnGap * i);

            }
        }
       
        foreach (var item in verticalPlatform)
        {
         
        }

       

    
    }
    private Transform ReturnSpawnPosition()
    {
        if(isFlipped)
        {
            return topYPosition;
        }
        else
        {
            return bottomYPosition;
        }
    }
 
}