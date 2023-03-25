using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronologicalPlatformManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] platform;
  [SerializeField]  int previousPlatform = 0;
    [SerializeField] int totalPlatforms;
    public bool isTimed;
  public  float timeUntilCrumble;


    private void Start()
    {
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
}
