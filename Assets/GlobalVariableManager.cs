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
       
    }
    public void SaveData(GameData gameData)
    {
        gameData.mainLight1a= mainLight1a;
       
    }
}
