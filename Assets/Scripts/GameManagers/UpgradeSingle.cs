using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSingle : MonoBehaviour
{
    public Upgrades upgrades;
    string gameObjectName, description;
    private int upgradeNumber;
  

    private void Start()
    {
        gameObjectName = upgrades.gameObjectName; description = upgrades.description;
        upgradeNumber = upgrades.upgradeNumber;
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ProgressionManager.instance.progression[ReturnNumber()] = true;
        }
       
    }
    
    int ReturnNumber()
    {
        return upgradeNumber;
    }
}
