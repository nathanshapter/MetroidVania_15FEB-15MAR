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
        Invoke("turnOn", 3);
    }
    void turnOn()
    {
        isOn= true;
        gls.CheckSwitches();
    }
}
