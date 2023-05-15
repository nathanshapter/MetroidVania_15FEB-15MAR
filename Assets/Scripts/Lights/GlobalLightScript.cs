using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GlobalLightScript : MonoBehaviour
{
    [SerializeField] bool startOff;
     bool lightOn;
    Light2D globalLight;
  public  GlobalLightSwitch[] switches;
    public LightFlicker[] torches;
    int totalSwitches;

    string sceneName; 
    void Start()
    {
        torches = FindObjectsOfType<LightFlicker>();


        switches = FindObjectsOfType<GlobalLightSwitch>();

        sceneName = SceneManager.GetActiveScene().name.ToString();
        print($"sceneName being used is {sceneName}");
        
        totalSwitches = switches.Length;
        numberOfSwitchesOn = totalSwitches;
        globalLight = GetComponent<Light2D>();

        
        if (startOff &&  !GlobalVariableManager.instance.globalLights[sceneName])
        {
            globalLight.intensity = 0;

            if(switches.Length == 0)
            {
               
                

                switches = FindObjectsOfType<GlobalLightSwitch>();
                Debug.Log(torches.Length);
            }
            
            
        }
        else 
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
            print("All switches have been destroyed");
        }
        else
        {
            print($" There are still {switches.Length} switches to destroy!");
        }
        
    } 

    public void LightFadeIn() 
    {
    
       
        if(sceneName != null)
        {
            GlobalVariableManager.instance.globalLights[sceneName] = true;
        }
        
       



        lightOn = true;
       
        
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
