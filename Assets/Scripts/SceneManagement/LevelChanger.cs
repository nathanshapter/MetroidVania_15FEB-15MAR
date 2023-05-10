using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] LevelConnection connection;

    [SerializeField] string targetSceneName;
    [SerializeField] Transform spawnPoint;
    LevelManager levelManager;

    private void Start()
    {
        levelManager= FindObjectOfType<LevelManager>();
        if(connection == LevelConnection.ActiveConnection) {
            FindObjectOfType<PlayerMovement>().transform.position = spawnPoint.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            LevelConnection.ActiveConnection = connection;
            levelManager.LoadScene(targetSceneName);
           //   SceneManager.LoadScene(targetSceneName);
        }
            
        }



  
}


