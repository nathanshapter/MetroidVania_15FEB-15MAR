using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour, iSaveData
{
    [SerializeField] private GameObject loadercanvas;
    [SerializeField] private Image progressBar;
    [SerializeField] int waitBeforeLoad;
    PlayerMovement player;
   

    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
      player = FindObjectOfType<PlayerMovement>(); // play animation of running into next scene
       // print("Level Manager loaded");
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
       
    }
  
    public void SaveData(GameData gameData)
    {
       // this needs to save the progression, but does not need to load it
    }
    public void LoadData(GameData gameData)
    {

    }

    public async void LoadScene(string sceneName)
    {
        
        if(player!=null)
        {
            player.DisableMovement(1); // animation to play when leaving
        }
       

        var scene = SceneManager.LoadSceneAsync(sceneName);
        
        scene.allowSceneActivation = false;

        if(loadercanvas!= null)
        {
            loadercanvas.SetActive(true);
        }
        
       
        do {
            await Task.Delay(10);
       //     progressBar.fillAmount = scene.progress;

           

        } while (scene.progress < 0.9f);


        await Task.Delay (waitBeforeLoad * 1000);
        scene.allowSceneActivation = true;
        if (loadercanvas != null)
            loadercanvas.SetActive(false);





    }


   
   // commented out things need to be added into the in between levels, so that we can fade in/out 
}
