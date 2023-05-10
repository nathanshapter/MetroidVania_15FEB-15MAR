using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    string scene;

   public void ChangeScene(string sceneName)
    {
        LevelManager.Instance.LoadScene(sceneName);
       scene= sceneName;
    }


 

}
