using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSceneController : MonoBehaviour
{
    PlayerMovement playerMovement;
    DeathManager deathManager;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        deathManager = FindObjectOfType<DeathManager>();
        print(playerMovement.gameObject.transform.position);
        print($"You have died {deathManager.totalDeaths} times");
    }
}
