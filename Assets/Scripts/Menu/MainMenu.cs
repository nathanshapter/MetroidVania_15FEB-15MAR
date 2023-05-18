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
    string scene;

    private void Start()
    {
        if (!SaveDataManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }

    }
    public void SaveData(GameData data)
    {
        scene = SceneManager.GetActiveScene().name;
        data.sceneName = scene;

    }
    public void LoadData(GameData data) // this just grabs the scene on OnContinue
    {
        scene = data.sceneName;
    }
 
    public void OnNewGameClicked()
    {
        DisableMenuButtons();
       
        SceneManager.LoadSceneAsync("Level 1a"); // this needs to be changed to deathscene, once it is running
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
  

  
}