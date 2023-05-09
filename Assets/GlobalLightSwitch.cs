using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLightSwitch : MonoBehaviour
{
    public bool isOn;
    GlobalLightScript gls;


    private void Start()
    {
        gls = GetComponentInParent<GlobalLightScript>();
       
    }
    void turnOn()
    {

        isOn= true;
        gls.CheckSwitches();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Array.Resize(ref gls.switches, gls.switches.Length -1);
        print(gls.switches.Length);
        gls.CheckSwitches();
        Destroy(gameObject);
    }
}
