using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightScript : MonoBehaviour
{
    [SerializeField] bool startOff;
    [SerializeField] bool lightOn;
    Light2D globalLight;
  public  GlobalLightSwitch[] switches;
    int totalSwitches;

    void Start()
    {

        print("here");
        switches = FindObjectsOfType<GlobalLightSwitch>();
        totalSwitches = switches.Length;
        numberOfSwitchesOn = totalSwitches;
        globalLight = GetComponent<Light2D>();
        if(startOff && switches.Length >0 && !GlobalVariableManager.instance.mainLight1a)
        {
            globalLight.intensity = 0;
            
        }
        else if(GlobalVariableManager.instance.mainLight1a == true)
        {
          LightFadeIn();
           
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
        
    } 

    private void LightFadeIn() 
    {
        lightOn= true;
        GlobalVariableManager.instance.mainLight1a = true;
        print(GlobalVariableManager.instance.mainLight1a);
        DOTween.To(() => globalLight.intensity, x => globalLight.intensity = x, 0.5f, 6);
        foreach(GlobalLightSwitch i in switches)
        {
            i.gameObject.SetActive(false);
        }
    }


}
