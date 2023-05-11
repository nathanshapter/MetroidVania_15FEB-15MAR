using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightScript : MonoBehaviour
{
    [SerializeField] bool startOff;
    Light2D globalLight;
  public  GlobalLightSwitch[] switches;
    int totalSwitches;

    void Start()
    {
        switches = FindObjectsOfType<GlobalLightSwitch>();
        totalSwitches = switches.Length;
        numberOfSwitchesOn = totalSwitches;
        globalLight = GetComponent<Light2D>();
        if(startOff && switches.Length >0)
        {
            globalLight.intensity = 0;
        }
       
       
    }
  


    int numberOfSwitchesOn;

   
    public void CheckSwitches()
    {
     
        if(switches.Length == 0)
        {
            LightFadeIn();
        }
        else
        {
            print($" There are still {switches.Length} switches to destroy!");
        }
        
    } // DONT MAKE IT A SWITCH THATS BORING, MAKE IT A BLOCK THAT YOU MUST DESTROY

    private void LightFadeIn() 
    {
        DOTween.To(() => globalLight.intensity, x => globalLight.intensity = x, 0.4f, 6);
    }


}
