using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStaticShooter : MonoBehaviour
{
  [SerializeField]  Transform player;

    [SerializeField] float aggroRange, aggroAwakeRange;
    bool awake = false;
    [SerializeField] float rotationOffset;


    float distanceBetweenPlayer;
    Transform target; // player


    [Header("Ball Properties")]
    [SerializeField]  int amountOfBallsSpawned, amountOfBallsToSpawn;
    [SerializeField] float timeBetweenFireballSpawn;
    [SerializeField] GameObject fireball;
    [SerializeField] Transform fireballSpawn;
   [SerializeField] float timeSinceLastWave;
    [SerializeField] float timeBetweenWaves = 5f;
    bool waveInProgress = false;

    public float ballspeed;
    private void Awake()
    {
        target = FindObjectOfType<Health>().gameObject.transform;
    }
    private void Start()
    {
        StartCoroutine(SpawnFireBalls());
    }
    private void Update()
    {
        
        distanceBetweenPlayer = Vector3.Distance(target.transform.position, transform.position);
        print(distanceBetweenPlayer);
        LookAtPlayer();
        if (!waveInProgress)
        {
            timeSinceLastWave += Time.deltaTime;
        }
        
        if(timeSinceLastWave >= timeBetweenWaves)
        {
            if(!waveInProgress)
            {
                StartCoroutine(SpawnFireBalls());
            }
            

        }
    }
    IEnumerator SpawnFireBalls()
    {
        waveInProgress= true;
       
        if (amountOfBallsSpawned < amountOfBallsToSpawn)
        {
            for (amountOfBallsSpawned = 0; amountOfBallsSpawned < amountOfBallsToSpawn; amountOfBallsSpawned++)
            {              
               
                
                    yield return new WaitForSeconds(timeBetweenFireballSpawn);
                    Instantiate(fireball, fireballSpawn.position, fireballSpawn.transform.rotation);

                

            }
            timeSinceLastWave = 0;
            waveInProgress= false;
            amountOfBallsSpawned = 0;
        }
    }
    public void LookAtPlayer()
    {
        var dir = target.position - this.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + rotationOffset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
