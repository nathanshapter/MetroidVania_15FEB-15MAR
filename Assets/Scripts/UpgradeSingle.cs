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
            if(upgradeNumber == 1)
            {
                progressionManager.progression[1] = true;
            }
            if (upgradeNumber == 2)
            {
                progressionManager.progression[2] = true;
            }
            if (upgradeNumber == 3)
            {
                progressionManager.progression[3] = true;
            }
            if (upgradeNumber == 4)
            {
                progressionManager.progression[4] = true;
            }
        }
    }
}
