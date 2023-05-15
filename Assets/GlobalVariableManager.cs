using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalVariableManager : MonoBehaviour, iSaveData
{
    public static GlobalVariableManager instance { get; private set; }

    [Header("All")]
    [SerializeField] float numberTimer;





    [Header("Level 1j")]
    [SerializeField] public bool placeHolderBool;


    int test = -500;

    string level1a = "Level 1a";
    string level1b = "Level 1b";


    string baseName = "Level 1";
    public SerialisableDictionary<string, bool> globalLights = new SerialisableDictionary<string, bool>();

    bool testbool;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }



        //  Debug.Log($"sceneb is on?{globalLights["sceneB"]}" );
        //   globalLights["sceneB"] = true;


      //  globalLights.Add(level1a, true);
       // globalLights.Add(level1b, true);
     
        for (char c = 'a'; c <= 'z'; c++)
        {
            string sceneName = baseName + c;
         //   Debug.Log(sceneName);
            globalLights.Add(sceneName, false);
          
        }
        for(char c = 'a'; c <= 'z'; c++)
        {
            for(char d = 'a'; d <='z'; d++)
            {
                string sceneName2 = baseName + c + d;
               // Debug.Log(sceneName2);

                globalLights.Add(sceneName2, false);
             
            }
            
        }
        globalLights["Level 1xy"] = true;
       

    }
   
    private void Update()
    {
        numberTimer += Time.deltaTime;
    }

    public void LoadData(GameData gameData)
    {
        Debug.Log($"Level 1a was loaded as {gameData.switches["Level 1a"]}");

        if (gameData.switches.Count == 0)
        {
            gameData.switches = globalLights;
            Debug.Log($" Saved data of global lights was {0}, now it is {gameData.switches.Count}");
        }
        else
        {
            this.globalLights = gameData.switches;
            Debug.Log($"Global lights were successfully loaded at count: {gameData.switches.Count} for debugging, the value of" +
                $"Level 1xy = {gameData.switches["Level 1xy"]} ");

        }



        if (numberTimer> 5)
        {
            SaveData(gameData);
        }

        if (globalLights[level1a] == true  && SceneManager.GetActiveScene().name == level1a)
        {            
            FindObjectOfType<GlobalLightScript>().LightFadeIn();
        }
        if (globalLights[level1b] == true && SceneManager.GetActiveScene().name == level1b)
        {
            FindObjectOfType<GlobalLightScript>().LightFadeIn();
        }
      
       
    }
    public void SaveData(GameData gameData)
    {
        gameData.switches = this.globalLights;
        Debug.Log("Saved switches in dictionary with count: "+gameData.switches.Count);
        Debug.Log($"Level 1a was saved as {gameData.switches["Level 1a"]}");
    }
   
}
