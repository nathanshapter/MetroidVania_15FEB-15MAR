using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronologicalPlatform : MonoBehaviour
{
    ChronologicalPlatformManager manager;
    bool hasSpawnedNextPlatform = false;
   [SerializeField] bool isFirstPlatform = false;

    bool isCrumbleTimed;
     float timeUntilCrumble;
    float timeUntilCrumbleTimer;


    bool disablePlatformOnJump = false;
    bool spawnAll = false;


   bool letPlatformsRespawn;
    float platformRespawnTime;

    bool spawnAsTimer;
    private void Start()
    {
        manager = GetComponentInParent<ChronologicalPlatformManager>();       
            isCrumbleTimed = GetComponentInParent<ChronologicalPlatformManager>().isCrumbleTimed;
            timeUntilCrumble = GetComponentInParent<ChronologicalPlatformManager>().timeUntilCrumble;
        disablePlatformOnJump = GetComponentInParent<ChronologicalPlatformManager>().disablePlatformOnJump;
        spawnAll = GetComponentInParent<ChronologicalPlatformManager>().spawnAll;
        letPlatformsRespawn = GetComponentInParent<ChronologicalPlatformManager>().letPlatformsRespawn;
        platformRespawnTime = GetComponentInParent<ChronologicalPlatformManager>().platformRespawnTime;
        spawnAsTimer = GetComponentInParent<ChronologicalPlatformManager>().spawnAsTimer;
    }
    private void Update()
    {
        timeUntilCrumbleTimer += Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player") && !hasSpawnedNextPlatform && !manager.spawnAsTimer)
        {
            manager.ProcessPlatforms();
            
            hasSpawnedNextPlatform = true;
            if(isCrumbleTimed && !isFirstPlatform)
            {
                StartCoroutine(DisableTimer());
                
            }
           
        }
        if(isCrumbleTimed && !spawnAsTimer)
        {
           StartCoroutine( ProcessCrumble());
           
        }
        if(isFirstPlatform&& spawnAll)
        {
            manager.SpawnAll();
        }
        
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Player") && !isFirstPlatform &&  !manager.isCrumbleTimed &&disablePlatformOnJump && !manager.spawnAsTimer)
        {
            
            StartCoroutine(  Disable());
        }
        

        
        
    }
    private IEnumerator DisableTimer()
    {
        yield return new WaitForSeconds(timeUntilCrumble);
       
       StartCoroutine( Disable());
    }
    IEnumerator Disable()
    {

     
        
            
            yield return new WaitForSeconds(0);
      //  this.gameObject.SetActive(false);
           GetComponentInChildren<Renderer>().enabled = false;
           GetComponent<BoxCollider2D>().enabled = false;

        







    }
    IEnumerator ProcessCrumble()
    {
        timeUntilCrumbleTimer = 0;
        print(timeUntilCrumbleTimer);
        yield return new WaitForSeconds(timeUntilCrumble);
        GetComponentInChildren<Renderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
       
        if(letPlatformsRespawn)
        {
            yield return new WaitForSeconds(platformRespawnTime);
            GetComponentInChildren<Renderer>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
        }
           
        
    }

   

}
