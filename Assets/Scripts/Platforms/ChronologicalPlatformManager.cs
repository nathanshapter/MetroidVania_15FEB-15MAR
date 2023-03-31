using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronologicalPlatformManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] platform;
  [SerializeField]  int previousPlatform = 0;
    [SerializeField] int totalPlatforms;
    public bool isCrumbleTimed;
  public  float timeUntilCrumble;
    public bool spawnAsTimer = false;
    [SerializeField] float timeBetweenPlatformSpawn;
    [SerializeField] float timeInBetweenWaves;

   
    private void Start()
    {
       
        if (spawnAsTimer)
        {
            StartCoroutine(SpawnWaves());

        }



        foreach (var item in platform)
        {
            item.gameObject.SetActive(false);
        }
        platform[0].gameObject.SetActive(true);
        totalPlatforms = platform.Length;

        

    }

    private void Update()
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
                item.GetComponentInChildren<Renderer>().enabled = true;
                item.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        
      



    }

    int gizmoNumber=0;
    private void OnDrawGizmos()
    {       
       
        Gizmos.color = Color.green;

        foreach (var item in platform)
        {          
            if(gizmoNumber +1  >= platform.Length)
            {
                gizmoNumber = 0;
            }
            Gizmos.DrawLine(platform[gizmoNumber].transform.position, platform[gizmoNumber +1].transform.position);
            gizmoNumber++;


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
        
        StartCoroutine(SpawnWaves());
    }

}
