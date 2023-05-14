using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalReverseSwitch : MonoBehaviour
{
    GlobalLightScript gls;

    private void Start()
    {


        gls = GetComponentInParent<GlobalLightScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
}
