using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class ChronologicalPlatformManager : MonoBehaviour
{
    [SerializeField]  GameObject[]       platform;

    int               previousPlatform = 0;                                // tracks platforms in the foreach 
    int               totalPlatforms;                                      // is set automatically, used to reset previous platform

    public bool       startAllSpawned = false;
    public bool       spawnAsTimer = false;
    [SerializeField]  float             timeBetweenPlatformSpawn;         // timer in which the next platform is spawned, if spawned as timer
    [SerializeField]  float             timeInBetweenWaves;               // time that it will wait before respawning/despawning all platform
 
    
    public bool       isCrumbleTimed;                                      
    public float      timeUntilCrumble;                                   // while in contact with an object of tag "Player" this will countdown and then disable the object


    public bool disablePlatformOnJump = false;
    public bool spawnAll = false;
    private void Start()
    {
       
        if (spawnAsTimer)
        {
            StartCoroutine(SpawnWaves());
        }

        if(!startAllSpawned)
        {
            foreach (var item in platform) // turns of all platforms, and then the first back on
            {
                item.gameObject.SetActive(false);
            }

            platform[0].gameObject.SetActive(true);
            totalPlatforms = platform.Length;
        }

        if (startAllSpawned)
        {
            spawnAsTimer = false;
        }
    }

    private void Update() // debugging method to reenable all platform
    {
         Renable();
        
    }
    public void ProcessPlatforms()
    {


        if (previousPlatform +1 >= totalPlatforms) { previousPlatform = 0; previousPlatform--; }


        platform[previousPlatform + 1].gameObject.SetActive(true);
     

        previousPlatform++;
     

      
    }

  
    void Renable()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            foreach (var item in platform)
            {
               
            }
        }     
    }

  
    IEnumerator SpawnWaves()
    {
        int currentPlatform = 0;

        foreach (var item in platform)
        {
            platform[currentPlatform].SetActive(true);
            
            currentPlatform++;
            yield return new WaitForSeconds(timeBetweenPlatformSpawn);
        }

        currentPlatform = 0;
        

        yield return new WaitForSeconds(timeInBetweenWaves);
        foreach (var item in platform)
        {
            platform[currentPlatform].SetActive(false);

            currentPlatform++;
            yield return new WaitForSeconds(timeBetweenPlatformSpawn);
        }
        yield return new WaitForSeconds(timeInBetweenWaves);
        
        StartCoroutine(SpawnWaves()); // used to go on forever
    }

    public void SpawnAll()
    {
        int currentPlatform = 0;
        foreach (var item in platform)
        {
            item.GetComponentInChildren<Renderer>().enabled = true;
            item.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    int gizmoNumber = 0;
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
