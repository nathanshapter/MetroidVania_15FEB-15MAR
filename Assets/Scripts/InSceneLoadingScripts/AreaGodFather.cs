using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaGodFather : MonoBehaviour
{
  [SerializeField] GameObject[] area;
    [SerializeField] GameObject spawnArea;
    

    private void Start()
    {
        DisableAllAreasButFirst();
    }
  public  void DisableAllAreasButFirst()
    {
        foreach (GameObject i in area)
        {
            i.gameObject.SetActive(false);
           
        }
        area[0].SetActive(true);
        spawnArea.SetActive(true);
    }

   
}
