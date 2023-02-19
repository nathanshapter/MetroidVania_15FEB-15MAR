using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaGodFather : MonoBehaviour
{
  [SerializeField]  GameObject[] area;

    private void Start()
    {
        DisableAllAreas();
    }
  public  void DisableAllAreas()
    {
        foreach (GameObject i in area)
        {
            i.gameObject.SetActive(false);
        }
        area[0].SetActive(true);
    }
}
