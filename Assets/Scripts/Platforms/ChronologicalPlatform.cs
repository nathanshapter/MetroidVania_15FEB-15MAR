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


    bool disablePlatformOnJump = false;
    bool spawnAll = false;


    bool letPlatformsRespawn;
    float platformRespawnTime;

    bool spawnAsTimer;
    private void Start()
    {
        #region gets from manager
        manager = GetComponentInParent<ChronologicalPlatformManager>();
        isCrumbleTimed = GetComponentInParent<ChronologicalPlatformManager>().isCrumbleTimed;
        timeUntilCrumble = GetComponentInParent<ChronologicalPlatformManager>().timeUntilCrumble;
        disablePlatformOnJump = GetComponentInParent<ChronologicalPlatformManager>().disablePlatformOnJump;
        spawnAll = GetComponentInParent<ChronologicalPlatformManager>().spawnAll;
        letPlatformsRespawn = GetComponentInParent<ChronologicalPlatformManager>().letPlatformsRespawn;
        platformRespawnTime = GetComponentInParent<ChronologicalPlatformManager>().platformRespawnTime;
        spawnAsTimer = GetComponentInParent<ChronologicalPlatformManager>().spawnAsTimer;
        #endregion
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
        if (isCrumbleTimed && !spawnAsTimer && other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ProcessCrumble());

        }
        if (isFirstPlatform && spawnAll && other.gameObject.CompareTag("Player"))
        {
            manager.SpawnAll();
        }

    }
    private void OnCollisionExit2D(Collision2D other)
    {        
        if (other.gameObject.CompareTag("Player") && !isFirstPlatform &&  !manager.isCrumbleTimed &&disablePlatformOnJump && !manager.spawnAsTimer)
        {            
            Disable();
        }       
  
    }
    private IEnumerator DisableTimer()
    {
        yield return new WaitForSeconds(timeUntilCrumble);
       
        Disable();
    }
    void Disable()
    {
        ComponentEnable(false);
    }
    IEnumerator ProcessCrumble()
    {      
        yield return new WaitForSeconds(timeUntilCrumble);
        ComponentEnable(false);
        if (letPlatformsRespawn)
        {
            yield return new WaitForSeconds(platformRespawnTime);
            ComponentEnable(true);
        }          
        
    }

   void ComponentEnable(bool enable)
    {
        GetComponentInChildren<Renderer>().enabled = enable;
        GetComponent<BoxCollider2D>().enabled = enable;
    }

}
