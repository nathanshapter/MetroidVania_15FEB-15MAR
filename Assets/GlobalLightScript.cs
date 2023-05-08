using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightScript : MonoBehaviour
{
    [SerializeField] bool startOff;
    Light2D globalLight;
    [SerializeField] GlobalLightSwitch[] switches;
    int totalSwitches;

    void Start()
    {
        globalLight = GetComponent<Light2D>();
        if(startOff)
        {
            globalLight.intensity = 0;
        }
        switches = FindObjectsOfType<GlobalLightSwitch>();
        totalSwitches= switches.Length;
    }
    int numberRef =-1;
    int numberOfSwitchesOn = 0;
    public void CheckSwitches()
    {
       if( switches[numberRef +1].isOn== true)
        {
            numberOfSwitchesOn++;
        };
        if(numberOfSwitchesOn >= totalSwitches)
        {
            globalLight.intensity = .4f;
        }
        
    }

}
