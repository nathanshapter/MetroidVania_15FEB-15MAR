using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLightSwitch : MonoBehaviour, iSaveData
{
    [ContextMenu("Generate guid for id")]
    private void GenerateGUID()
    {
        id = System.Guid.NewGuid().ToString();
    }
    // IF LIGHTS ARE TURNING ON WHEN THEY ARE SUPPOSED TO BE IT IS BECAUSE THEIR ID IS THE SAME AS ANOTHER
    public bool isOn;
    GlobalLightScript globalLightScript;
    [SerializeField] private string id;

    private void Start()
    {

        globalLightScript = GetComponentInParent<GlobalLightScript>();
        if (isOn)
        {
           gameObject.SetActive(false);
        }

    }

    public void SaveData(GameData data)
    {

        
       if (data.switchesPressed.ContainsKey(id))
        {
            data.switchesPressed.Remove(id);
        }
        data.switchesPressed.Add(id, isOn);

    }
    

 

    public void LoadData(GameData data)
    {
        data.switchesPressed.TryGetValue(id, out isOn);
        if (isOn)
        {
               gameObject.SetActive(false);
            Debug.Log($"My id is {id} if I am on when I shouldn't be, it is because I have the same ID as another light");
        }

    }
  
 


    
    void turnOn()
    {

        isOn= true;
        globalLightScript.CheckSwitches();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
       
            Array.Resize(ref globalLightScript.switches, globalLightScript.switches.Length - 1);
            print(globalLightScript.switches.Length);
            globalLightScript.CheckSwitches();
            isOn = true;
            gameObject.SetActive(false);
            
        }

      
    }
    
}
