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
    [SerializeField] float lightIntensityx = 0.5f;
    [SerializeField] float lightIntensityFadeInx =6;
    int totalSwitches;

    string sceneName; 
    void Start()
    {
        torches = FindObjectsOfType<LightFlicker>();
        switches = FindObjectsOfType<GlobalLightSwitch>();
        sceneName = SceneManager.GetActiveScene().name.ToString();
        
        totalSwitches = switches.Length;    
        globalLight = GetComponent<Light2D>();

        
        if (startOff &&  !GlobalVariableManager.instance.globalLights[sceneName]) // if startoff is checked, and the saved value in gvm is false, turn the light off
        {
            globalLight.intensity = 0;

            if(switches.Length == 0) // try instantiating them, at a random spot if they should be spawning // should probably use iSave
                // iSaveData to load the amount, if it doesnt work correctly, then spawn them in the world
            {
               
                // this doesnt work correctly because they are not enabled. need to enable them if its 0, somehow

                switches = FindObjectsOfType<GlobalLightSwitch>();
                Debug.Log(switches.Length);
            }
            
            
        }
        else 
        {
          LightFadeIn();
         
           
        }      

      CheckSwitches();
    } 
   
    public void CheckSwitches()
    {     
        if(switches.Length == 0)
        {
            LightFadeIn();            
        }
        else
        {
            print($" There are still {switches.Length} switches to destroy!"); // keep this in until UI added
        }
        
    } 

    public void LightFadeIn() 
    {    
       
        if(sceneName != null)
        {
            GlobalVariableManager.instance.globalLights[sceneName] = true;
        }      
       
        lightOn = true;
       
        
        DOTween.To(() => globalLight.intensity, x => globalLight.intensity = x, lightIntensityx, lightIntensityFadeInx);
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
