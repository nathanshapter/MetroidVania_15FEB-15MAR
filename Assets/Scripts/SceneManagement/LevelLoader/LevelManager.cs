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

    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

     //   loadercanvas.SetActive(true);
     //   do {
            await Task.Delay(0);
       //     progressBar.fillAmount = scene.progress;
                
                
                
           //     } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
      //  loadercanvas.SetActive(false);
    }
   // commented out things need to be added into the in between levels, so that we can fade in/out 
}
