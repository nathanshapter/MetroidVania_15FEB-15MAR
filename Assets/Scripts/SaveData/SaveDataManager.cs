using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveDataManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
   public static SaveDataManager instance { get; private set; }
    private GameData gameData;
    private List<iSaveData> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    [SerializeField] private bool useEncryption;

  //  [ContextMenu("Erase all Data")]
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance != null)
        {
            Debug.LogError("Found more than one SaveDataManager in the scene");
        }

        instance = this; 
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
        StartCoroutine(SaveGameCo());
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        // need to load saved data from a file using data handler
        //if no data can be loaded, initiliase to a new game

        if(this.gameData == null)
        {
            Debug.Log("No Data was found. Initialiing data to default");
            NewGame();
        }
        // push loaded data to other scripts that need it

        foreach(iSaveData dataPersistenceobj in dataPersistenceObjects)
        {
            dataPersistenceobj.LoadData(gameData);
        }
        Debug.Log($"Loaded death count = {gameData.deathCount} ");
    }
  IEnumerator SaveGameCo()
    {
        yield return new WaitForSeconds(30);
        SaveGame();
        StartCoroutine(SaveGameCo());

    }

    public void SaveGame()
    {
        // first pass data to other scripts so they can update it
        foreach (iSaveData dataPersistenceobj in dataPersistenceObjects)
        {
            dataPersistenceobj.SaveData(ref gameData);
        }
        //save data to a file using data handler 
        Debug.Log($"Saved death count = {gameData.deathCount} ");

        dataHandler.Save(gameData);
    }
    private void OnApplicationQuit()
    {
        print("yes");
        SaveGame();
    }
    private List<iSaveData> FindAllDataPersistenceObjects()
    {
        IEnumerable<iSaveData> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<iSaveData>();

        return new List<iSaveData>(dataPersistenceObjects);
    }
}
