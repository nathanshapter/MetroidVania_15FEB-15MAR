using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{


  //  [SerializeField] private GameObject loaderCanvas;
  //  [SerializeField] private Image progressBar;
    public static SceneManagement Instance;
    private float target;

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
      //  target= 0;
     //   progressBar.fillAmount= 0;
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation= false;

     //   loaderCanvas.SetActive(true);
     //      do
    //    {

       //       target = scene.progress;
          //      } while (scene.progress < 0.9f);
        
        scene.allowSceneActivation = true;
     //   loaderCanvas.SetActive(false);
    }

    private void Update()
    {
    //    progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, 3* Time.deltaTime);
    }
    // to uncomment when fillbar is added in
}
