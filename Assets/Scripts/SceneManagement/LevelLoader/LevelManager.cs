using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
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
  

    public async void LoadScene(string sceneName)
    {
        player.DisableMovement(1); // animation to play when leaving

        var scene = SceneManager.LoadSceneAsync(sceneName);
        
        scene.allowSceneActivation = false;

        loadercanvas.SetActive(true);
       
        do {
            await Task.Delay(10);
       //     progressBar.fillAmount = scene.progress;

           

        } while (scene.progress < 0.9f);


        await Task.Delay (waitBeforeLoad * 1000);
        scene.allowSceneActivation = true;
        loadercanvas.SetActive(false);





    }


   
   // commented out things need to be added into the in between levels, so that we can fade in/out 
}
