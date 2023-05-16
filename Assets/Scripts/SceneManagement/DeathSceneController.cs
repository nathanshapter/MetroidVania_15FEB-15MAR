using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSceneController : MonoBehaviour, iSaveData
{
    PlayerMovement playerMovement;
  
    string sceneToReloadTo;


    LevelChanger lc;
    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        
       
        print(playerMovement.gameObject.transform.position);
        print($"You have died {DeathManager.Instance.totalDeaths} times");

        playerMovement.transform.position = FindObjectOfType<RespawnManager>().spawnPositions[0].transform.position;

       
      //  StartCoroutine(ReturnPlayerToScene());

        lc = FindObjectOfType<LevelChanger>();
        lc.targetSceneName = sceneToReloadTo;
    }


 

    public void LoadData(GameData gameData)
    {
        sceneToReloadTo = gameData.sceneToReloadTo;
        print(sceneToReloadTo);
    }
    public void SaveData(GameData gameData)
    {

    }
}
