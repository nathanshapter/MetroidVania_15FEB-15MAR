using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariableManager : MonoBehaviour, iSaveData
{
    public static GlobalVariableManager instance { get; private set; }

    [Header("All")]
    [SerializeField] float numberTimer;


    [Header("Level 1a")]  
    [SerializeField] public bool mainLight1a;


    [Header("Level 1j")]
    [SerializeField] public bool placeHolderBool;


    int test = -500;

    Dictionary<string, bool> globalLights = new Dictionary<string, bool>() { 
        { "sceneA", true }, { "sceneB", false }, { "sceneC", true }, };

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
        




    }
   
    private void Update()
    {
        numberTimer += Time.deltaTime;
    }

    public void LoadData(GameData gameData)
    {
        if(numberTimer> 5)
        {
            SaveData(gameData);
        }

        if (globalLights["sceneA"] == true)
        {
          
            mainLight1a= true;
        }
      
       
    }
    public void SaveData(GameData gameData)
    {
        gameData.lightBool= mainLight1a;
       
    }
   public bool GetLight()
    {
        return mainLight1a; // i think we need to learn how to use dictionarys to do this
    }
}
