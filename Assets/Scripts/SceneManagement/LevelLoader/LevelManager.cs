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
    CanvasFade canvasFade;

    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
        canvasFade = FindObjectOfType<CanvasFade>();
        print("Level Manager loaded");
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
  

    public async void LoadScene(string sceneName)
    {
       // canvasFade.FadeIn();

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
