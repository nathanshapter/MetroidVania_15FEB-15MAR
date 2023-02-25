using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPositions;

    [SerializeField] GameObject fallingSpike;

    int spikesSpawned;
  [SerializeField]  int minSpikesToSpawn;
   [SerializeField] int maxSpikesToSpawn;


    float timeSinceLastDrop;
    private void Start()
    {
        SpawnSpikes();
    }

    private void SpawnSpikes()
    {
        foreach (Transform i in spawnPositions)
        {
            if (Random.Range(0, 10) > 4)
            {
                
                Instantiate(fallingSpike, i);
                spikesSpawned++;
                if(spikesSpawned>= maxSpikesToSpawn) { spikesSpawned = 0; break; }
            }

        }
    }


    private void Update()
    {
        timeSinceLastDrop += Time.deltaTime;
        if(timeSinceLastDrop > 5)
        {
            timeSinceLastDrop= 0;
            SpawnSpikes();
        }
    }
    public IEnumerator DropSpikes()
    {
                
    //        SpawnSpikes();

        yield return null;
    }
}
