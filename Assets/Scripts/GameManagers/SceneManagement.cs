using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private GameObject loaderCanvas;
    [SerializeField] private Image progressBar;
    public static SceneManagement Instance;

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
        scene.allowSceneActivation= false;

        loaderCanvas.SetActive(true);
        do
        {
            
            progressBar.fillAmount = scene.progress;
        } while (scene.progress < 0.9f);
        scene.allowSceneActivation = true;
        loaderCanvas.SetActive(false);
    }
}
