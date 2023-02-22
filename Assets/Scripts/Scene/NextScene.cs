using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{

    SceneManagement sm;
    [SerializeField] string sceneName;
    [SerializeField] bool _up, _down, _left, _right;
    OutSpawner os;
    // Start is called before the first frame update
    void Start()
    {
       sm = FindObjectOfType<SceneManagement>();
        os = FindObjectOfType<OutSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sm.LoadScene(sceneName);
            os.
        }
    }
    
}
