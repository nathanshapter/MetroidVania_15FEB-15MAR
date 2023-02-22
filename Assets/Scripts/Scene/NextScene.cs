using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{

    SceneManagement sm;
    [SerializeField] string sceneName;
    // Start is called before the first frame update
    void Start()
    {
       sm = FindObjectOfType<SceneManagement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sm.LoadScene(sceneName);
        }
    }
    
}
