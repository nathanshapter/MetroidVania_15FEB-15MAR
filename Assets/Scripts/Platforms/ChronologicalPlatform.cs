using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronologicalPlatform : MonoBehaviour
{
    ChronologicalPlatformManager manager;
    bool hasSpawnedNextPlatform = false;
   [SerializeField] bool isFirstPlatform = false;
    bool isTimed;
     float timeUntilCrumble;

    private void Start()
    {
        manager = GetComponentInParent<ChronologicalPlatformManager>();
        isTimed = GetComponentInParent<ChronologicalPlatformManager>().isTimed;
        timeUntilCrumble = GetComponentInParent<ChronologicalPlatformManager>().timeUntilCrumble;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasSpawnedNextPlatform)
        {
            manager.ProcessPlatforms();
            
            hasSpawnedNextPlatform = true;
            if(isTimed && !isFirstPlatform)
            {
                StartCoroutine(DisableTimer());
            }
           
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isFirstPlatform)
        {
            Disable();
        }
    }
    private IEnumerator DisableTimer()
    {
        yield return new WaitForSeconds(timeUntilCrumble);
        Disable();
    }
    private void Disable()
    {
        GetComponentInChildren<Renderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

  

}
