using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class ChronologicalPlatformManager : MonoBehaviour
{
   

    int               previousPlatform = 0;                                // tracks platforms in the foreach 
    int               totalPlatforms;                                      // is set automatically, used to reset previous platform

    public bool       startAllSpawned = false;
    public bool       spawnAsTimer = false;

    [SerializeField]  float timeBetweenPlatformSpawn;                    // timer in which the next platform is spawned, if spawned as timer
    [SerializeField]  float timeInBetweenWaves;                          // time that it will wait before respawning/despawning all platform
 
    
    public bool       isCrumbleTimed;                                      
    public float      timeUntilCrumble;                                   // while in contact with an object of tag "Player" this will countdown and then disable the object


    public bool       disablePlatformOnJump = false;                    // disable platform OncollisionExit
    public bool       spawnAll = false;                                 // spawn back all platforms that have been activated, after they crumbled, and after having jumped on the first platform

    public bool       letPlatformsRespawn;                              // if crumble is enabled, these two allows platforms to respawn
    public float      platformRespawnTime;

    ChronologicalPlatformArray cpa;
    private void Start()
    {
     cpa = GetComponent<ChronologicalPlatformArray>();

        if (spawnAsTimer)
        {
            StartCoroutine(SpawnWaves());
            isCrumbleTimed = false;
            letPlatformsRespawn= false;
        }
        #region bool resets
        if (!startAllSpawned)
        {
            foreach (var item in cpa.platform) // turns of all platforms, and then the first back on
            {
                item.gameObject.SetActive(false);
            }

            cpa.platform[0].gameObject.SetActive(true);
            totalPlatforms = cpa.platform.Length;
        }
        if(spawnAll)
        {
            spawnAsTimer = false;
        }
        if (startAllSpawned)
        {
            spawnAsTimer = false;
        }
       
        if(disablePlatformOnJump)
        {
            isCrumbleTimed = false;
        }
        if(isCrumbleTimed)
        {
            spawnAsTimer = false;
           
        }
        #endregion
    }

     // spawn all of the platforms chronologically 
    public void ProcessPlatforms()
    {

        if (previousPlatform +1 >= totalPlatforms) { previousPlatform = 0; previousPlatform--; }


        cpa.platform[previousPlatform + 1].gameObject.SetActive(true);
     

        previousPlatform++;    
              
    }   

    // used to spawn the platforms as a wave set by timeBetweenPlatformSpawn and timeInBetweenWaves
    IEnumerator SpawnWaves()
    {
        if (!startAllSpawned)
        {
            int currentPlatform = 0;

            foreach (var item in cpa.platform) // setting them all to true, and their components 
            {
                cpa.platform[currentPlatform].SetActive(true);
                cpa.platform[currentPlatform].gameObject.GetComponent<BoxCollider2D>().enabled = true;
                cpa.platform[currentPlatform].gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
                currentPlatform++;
                yield return new WaitForSeconds(timeBetweenPlatformSpawn);
            }

            currentPlatform = 0;


            yield return new WaitForSeconds(timeInBetweenWaves);
            foreach (var item in cpa.platform) // setting to false
            {
                cpa.platform[currentPlatform].SetActive(false);

                currentPlatform++;
                yield return new WaitForSeconds(timeBetweenPlatformSpawn);
            }
            yield return new WaitForSeconds(timeInBetweenWaves);

            StartCoroutine(SpawnWaves()); // used to go on forever 

        }

    }

    public void SpawnAll()
    {        
        foreach (var item in cpa.platform)
        {
            item.GetComponentInChildren<Renderer>().enabled = true;
            item.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    
   
}
