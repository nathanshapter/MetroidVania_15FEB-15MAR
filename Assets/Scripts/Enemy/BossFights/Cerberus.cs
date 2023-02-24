using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cerberus : MonoBehaviour
{
    [SerializeField] GameObject body, headTop, headMiddle, headBottom;
    [SerializeField] GameObject fireball;
    [SerializeField] Transform fireballSpawn;
    
    [SerializeField] float timeBetweenWaveSpawns = 5;
    [SerializeField] float timeBetweenFireballSpawn = 0.5f;
 [SerializeField]   float timeBetweenLastWave;
    public int fireballHits;
    [SerializeField] int amountOfBallsToSpawn = 5;
    int amountOfBallsSpawned =0;

  [SerializeField]  bool waveInProgress = false;
    

    private void Start()
    {
       
        
    }
    IEnumerator SpawnFireballs()
    {
        waveInProgress= true;
       
        if (amountOfBallsSpawned < amountOfBallsToSpawn)
        {
            for(amountOfBallsSpawned = 0; amountOfBallsSpawned < amountOfBallsToSpawn; amountOfBallsSpawned++)
            {
                yield return new WaitForSeconds(timeBetweenFireballSpawn);
                Instantiate(fireball, fireballSpawn.position, fireballSpawn.transform.rotation);
               
            }        
            
        }
        amountOfBallsSpawned = 0;
        timeBetweenLastWave = 0;
        waveInProgress = false;

    }
    
   
    private void Update()
    {
        timeBetweenLastWave += Time.deltaTime;
        if(timeBetweenLastWave > timeBetweenWaveSpawns && !waveInProgress)
        {
            print("started");
            StartCoroutine(SpawnFireballs());
        }
    }
  
}
