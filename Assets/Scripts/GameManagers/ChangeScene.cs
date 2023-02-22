using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
 

  public void ChangeSceneTest(string sceneName)
    {
        SceneManagement.Instance.LoadScene(sceneName);
    }
}
