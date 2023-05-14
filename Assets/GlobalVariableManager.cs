using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariableManager : MonoBehaviour, iSaveData
{
    public static GlobalVariableManager instance { get; private set; }

    [Header("Level 1a")]
  
    [SerializeField] public bool mainLight1a;

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
    }
   [SerializeField] float numberTimer;
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
        
        mainLight1a= gameData.mainLight1a;
        print($"gvm loaded the main light as {mainLight1a}");
    }
    public void SaveData(GameData gameData)
    {
        gameData.mainLight1a= mainLight1a;
        print($"gvm saved the main light as {mainLight1a}");
    }
}
