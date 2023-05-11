using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, iSaveData
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    
    private void Start()
    {
        if (!SaveDataManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }

    }
    string scene;
    public void OnNewGameClicked()
    {
        DisableMenuButtons();
        // create a new game - which will initialize our game data
        
       
        // load the gameplay scene - which will in turn save the game because of
        // OnSceneUnloaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync("Level 1a");
        SaveDataManager.instance.NewGame();
        SaveDataManager.instance.SaveGame();
    }

    public void OnContinueGameClicked()
    {
     
        
        SaveDataManager.instance.LoadGame();
       DisableMenuButtons();
       
        // load the next scene - which will in turn load the game because of 
        // OnSceneLoaded() in the DataPersistenceManager
       SceneManager.LoadSceneAsync(scene);
  
    } 

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
    public void LoadData(GameData data)
    {
        scene = data.sceneName;
       
    }

    public void SaveData(GameData data)
    {
     scene = SceneManager.GetActiveScene().name;
        data.sceneName= scene;

    }
}