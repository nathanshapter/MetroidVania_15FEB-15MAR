using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cerberus : MonoBehaviour
{
    [SerializeField] GameObject[] heads;
    [SerializeField] GameObject fireball;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform fireballSpawn;

     float timeBetweenWaveSpawns = 5;
     float timeBetweenFireballSpawn = 0.5f;
    [SerializeField] float timeBetweenLastWave;
    public int fireballHits;
    [SerializeField] int amountOfBallsToSpawn = 5;
    int amountOfBallsSpawned = 0;

    [SerializeField] bool waveInProgress = false;

   
    public int selfHit = 0;

    public CerberusStages[] cerberusStage;

    private int health;

    private void Start()
    {

        timeBetweenWaveSpawns = cerberusStage[0].timeBetweenWaveSpawns;
        timeBetweenFireballSpawn = cerberusStage[0].timeBetweenFireballSpawn;
        amountOfBallsToSpawn = cerberusStage[0].amountOfBallsToSpawn;
        
        
    }
    public void GetStageValues()
    {
        timeBetweenWaveSpawns = returnStage().timeBetweenWaveSpawns;
        timeBetweenFireballSpawn = returnStage().timeBetweenFireballSpawn;
        amountOfBallsToSpawn = returnStage().amountOfBallsToSpawn;

    }
  
    IEnumerator SpawnFireballs() // middle head logic
    {

        waveInProgress = true;

        if (amountOfBallsSpawned < amountOfBallsToSpawn)
        {
            for (amountOfBallsSpawned = 0; amountOfBallsSpawned < amountOfBallsToSpawn; amountOfBallsSpawned++)
            {
                if (selfHit <= 5)
                {
                    yield return new WaitForSeconds(timeBetweenFireballSpawn);
                    Instantiate(fireball, fireballSpawn.position, fireballSpawn.transform.rotation);
                }


            }

        }
        amountOfBallsSpawned = 0;
        timeBetweenLastWave = 0;
        waveInProgress = false;

    }


    private void Update()
    {
        health = GetComponent<EnemyHealth>().health;
        

        timeBetweenLastWave += Time.deltaTime;
        if (timeBetweenLastWave > timeBetweenWaveSpawns && !waveInProgress)
        {
            print("started");
            StartCoroutine(SpawnFireballs());
            selfHit = 0;
        }

        GetStageValues();

        
    }
    private CerberusStages returnStage()
    {
        if(health <= 100)
        {
            return cerberusStage[3];
        }
        if(health<= 200)
        {
            return cerberusStage[2];
        }
        if(health <= 300)
        {
            return cerberusStage[1];
        }
        else
        {
            return cerberusStage[0];
        }
    }
 
 

}
