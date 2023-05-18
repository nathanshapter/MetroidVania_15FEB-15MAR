using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalVariableManager : MonoBehaviour, iSaveData
{
    public static GlobalVariableManager instance { get; private set; }

    [Header("All")]
    [SerializeField] float numberTimer;

    string baseName = "Level 1";
    public SerialisableDictionary<string, bool> globalLights = new SerialisableDictionary<string, bool>();

 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        CreateSceneDictionary();

    }

    public void SaveData(GameData gameData)
    {
        gameData.globalLights = this.globalLights;

    }

    public void LoadData(GameData gameData)
    {
        LoadGlobalLights(gameData);
        if (numberTimer > 5)
        {
            SaveData(gameData);
        }
    }


    private void Update()
    {
        numberTimer += Time.deltaTime;
    }


    private void CreateSceneDictionary()
    {
        for (char c = 'a'; c <= 'z'; c++)
        {
            string sceneName = baseName + c;

            globalLights.Add(sceneName, false);

        }
        for (char c = 'a'; c <= 'z'; c++)
        {
            for (char d = 'a'; d <= 'z'; d++)
            {
                string sceneName2 = baseName + c + d;


                globalLights.Add(sceneName2, false);

            }

        }
    }  

    private void LoadGlobalLights(GameData gameData)
    {
        if (gameData.globalLights.Count == 0)
        {
            gameData.globalLights = globalLights;

        }
        else
        {
            this.globalLights = gameData.globalLights;

        }
    }

 
   
}
