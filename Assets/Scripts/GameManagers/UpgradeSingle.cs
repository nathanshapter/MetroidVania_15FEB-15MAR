using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSingle : MonoBehaviour
{
    public Upgrades upgrades;
    string gameObjectName, description;
    private int upgradeNumber;
    ProgressionManager progressionManager;

    private void Start()
    {
        gameObjectName = upgrades.gameObjectName; description = upgrades.description;
        upgradeNumber = upgrades.upgradeNumber;
        progressionManager = FindObjectOfType<ProgressionManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            progressionManager.progression[ReturnNumber()] = true;
        }
       
    }
    int ReturnNumber()
    {
        return upgradeNumber;
    }
}
