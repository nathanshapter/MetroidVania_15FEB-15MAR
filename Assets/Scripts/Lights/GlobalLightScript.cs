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
    public LightFlicker[] torches;
    int totalSwitches;
  

    void Start()
    {
       
        torches = FindObjectsOfType<LightFlicker>();
        switches = FindObjectsOfType<GlobalLightSwitch>();
        totalSwitches = switches.Length;
        numberOfSwitchesOn = totalSwitches;
        globalLight = GetComponent<Light2D>();

        
        if (startOff && switches.Length >0 && !GlobalVariableManager.instance.GetLight())
        {
            globalLight.intensity = 0;
            
        }
        else if(GlobalVariableManager.instance.GetLight() == true)
        {
          LightFadeIn();
            print("checl");
           
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
        
        DOTween.To(() => globalLight.intensity, x => globalLight.intensity = x, 0.5f, 6);
        foreach(GlobalLightSwitch i in switches)
        {
            i.gameObject.SetActive(false);
        }
        foreach(LightFlicker i in torches)
        {
            i.turnOffRandomly = false;
            i.turnOnDistance= 100;
        }
       
    }
  

}
