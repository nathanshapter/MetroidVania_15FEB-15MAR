using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cerberus : MonoBehaviour
{
    [SerializeField] GameObject body, headTop, headMiddle, headBottom;
    [SerializeField] GameObject fireball;
    [SerializeField] Transform fireballSpawn;
    [SerializeField] int amountOfFireballsToSpawn;
    [SerializeField] float timeBetweenLastWave, timeBetweenSpawns;
    public int fireballHits;


    private void Start()
    {
        SpawnFireballs();
    }
    void SpawnFireballs()
    {
        Instantiate(fireball, fireballSpawn.position ,fireballSpawn.transform.rotation);
    }
    
    
}