using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CerberusValues", menuName = "CerberusValues")]
public class CerberusStages : ScriptableObject
{
   

    [Header("Spike Values")]
    // spikes
   public int spikesToSpawn;
    [Range(1, 15)] public int timeBetweenSpikeWave;
   [Range(0,11)] public int spawnAmount;

    [Header("Fireball Values")]
    //fireball

    public float timeBetweenWaveSpawns;
    public float timeBetweenFireballSpawn;
    public int amountOfBallsToSpawn;




}

