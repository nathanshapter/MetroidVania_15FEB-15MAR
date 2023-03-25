using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronologicalPlatformManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] platform;
   
  

    private void Start()
    {
        foreach (var item in platform)
        {
            item.gameObject.SetActive(false);
        }
        platform[0].gameObject.SetActive(true);
    }


}
