using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{                                               // to change these valeus needs to be done in cerberusstage scriptable object
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] GameObject fallingSpike;
    public CerberusStages[] cerberusStage;
   [SerializeField] int health;
    [SerializeField] EnemyHealth eh;
    [SerializeField] Cerberus cerberus;
    float timeSinceLastDrop;
    private int spikesToSpawn;
    private int timeBetweenSpikeWave;
    private int spawnAmount;
    public bool canStart;
    private void Start()
    {
        health = eh.health;
        SpawnSpikes();
        spawnAmount = cerberusStage[0].spawnAmount;
        spikesToSpawn = cerberusStage[0].spikesToSpawn;
        timeBetweenSpikeWave = cerberusStage[0].timeBetweenSpikeWave;

    }

    private void SpawnSpikes()
    {
        foreach (Transform i in spawnPositions)
        {
            if (Random.Range(0, 10) < spawnAmount)
            {
                if (cerberus.isSleeping) { return; }
                Instantiate(fallingSpike, i);
               
               
            }

        }
    }


    private void Update()
    {
        if (!canStart) return;

        health = eh.health;

        timeSinceLastDrop += Time.deltaTime;
        if(timeSinceLastDrop > timeBetweenSpikeWave)
        {
            timeSinceLastDrop= 0;
            SpawnSpikes();
        }
        GetStageValues();
       
    }
    private CerberusStages returnStage()
    {
        if (health <= 100)
        {
            return cerberusStage[3];
        }
        if (health <= 200)
        {
            return cerberusStage[2];
        }
        if (health <= 300)
        {
            return cerberusStage[1];
        }
        else
        {
            return cerberusStage[0];
        }
    }
    public void GetStageValues()
    {
       timeBetweenSpikeWave = returnStage().timeBetweenSpikeWave;
        spikesToSpawn = returnStage().spikesToSpawn;
        spawnAmount= returnStage().spawnAmount;
       

    }
}
