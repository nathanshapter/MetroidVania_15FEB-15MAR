using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cerberus : MonoBehaviour
{

    [Header("AllHeads Logic")]

    [SerializeField] public GameObject[] heads;
    [SerializeField] Transform playerTransform;
    public CerberusStages[] cerberusStage;
    private int health;
    [SerializeField] Health target; // this is to locate the player

    [Header("TopHead Logic")]

    [Header("MiddleHead Logic")]

    // gets
    [SerializeField] GameObject fireball;
    [SerializeField] Transform fireballSpawn;
    [SerializeField] CerberusFireProjectile projectile;

    // inspector sets
    [SerializeField] int amountOfBallsToSpawn = 5;

    // variables
    [SerializeField] float timeBetweenLastFireballWave;
    [SerializeField] bool waveInProgress = false;

    float timeBetweenFireBallWaveSpawns = 5;
    float timeBetweenFireballSpawn = 0.5f;    
    public int fireballHits;    
    int amountOfBallsSpawned = 0;
    public int selfHit = 0;
    int amountOfDropsInWave;

    [Header("BottomHead Logic")]

    // gets
    [SerializeField] Rigidbody2D bottomHeadrb;
    [SerializeField] CircleCollider2D bottomHeadCircleCollider;
    [SerializeField] Vector2 bottomHeadOriginalPosition;
    [SerializeField] float distanceBeforeAttack = 12;
    [SerializeField] float timeInBetweenBites;

    // sets
    Vector2 moveDirection;
    // variables
    float distanceBetweenPlayer;
    public bool isBiting;
    private bool canBite = true;






    private void Start()
    {
        canBite = true;
        target = FindObjectOfType<Health>();
        timeBetweenFireBallWaveSpawns = cerberusStage[0].timeBetweenWaveSpawns;
        timeBetweenFireballSpawn = cerberusStage[0].timeBetweenFireballSpawn;
        amountOfBallsToSpawn = cerberusStage[0].amountOfBallsToSpawn;
        amountOfDropsInWave = cerberusStage[0].amountOfDropsInWave;

        StartCoroutine(SpawnFireballs());


     bottomHeadOriginalPosition = heads[2].transform.position.normalized;
    }
    public void GetStageValues()
    {
        timeBetweenFireBallWaveSpawns = returnStage().timeBetweenWaveSpawns;
        timeBetweenFireballSpawn = returnStage().timeBetweenFireballSpawn;
        amountOfBallsToSpawn = returnStage().amountOfBallsToSpawn;
        amountOfDropsInWave = returnStage().amountOfDropsInWave;

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
        timeBetweenLastFireballWave = 0;
        waveInProgress = false;
    }


    private void Update()
    {

        if (heads[2].transform.position.x <= 18 || heads[2].transform.position.y >= 2)
        {
            canBite = false;
            print("here");
        }
        else
        {
            canBite= true;
           
            
        }

        health = GetComponent<EnemyHealth>().health; // to locate player
        timeBetweenLastFireballWave += Time.deltaTime;
        BeginNewFireballWave();
        GetStageValues();
        print(transform.position);
        distanceBetweenPlayer = Vector3.Distance(target.transform.position, transform.position); // update distance in real time

        ProcessBite();

     

    }

    

    private void BeginNewFireballWave()
    {
        if (timeBetweenLastFireballWave > timeBetweenFireBallWaveSpawns && !waveInProgress)
        {

            StartCoroutine(SpawnFireballs());
            selfHit = 0;
        }
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


    // bottom head bite

  

    public IEnumerator Bite()
    {
        canBite= false;
        float biteSpeed = 100;
        Vector2 targetPosition = target.transform.position;
        moveDirection = (new Vector3(targetPosition.x, targetPosition.y) - heads[2].transform.position).normalized * biteSpeed;
       
        isBiting = true;
        print("hello"); // bite the player
        
        yield return new WaitForSeconds(1);
        
        // return head to original position
        
        isBiting = false;



        yield return new WaitForSeconds(timeInBetweenBites);
        canBite = true;
        print(canBite);

    }
    private void ProcessBite()
    {
        if (!canBite) { bottomHeadrb.velocity = new Vector2(bottomHeadOriginalPosition.x, bottomHeadOriginalPosition.y).normalized; return; }

        if (distanceBetweenPlayer >= distanceBeforeAttack)
        {
            // do nothing as of yet
        }
        else if (!isBiting && distanceBetweenPlayer < distanceBeforeAttack)
        {
            StartCoroutine(Bite());
        }

        if (isBiting)
        {
            print("isBiting");
            bottomHeadrb.velocity = new Vector2(moveDirection.x, moveDirection.y).normalized * 50;
        }
        else
        {
            print("is returning");
            bottomHeadrb.velocity = new Vector2(bottomHeadOriginalPosition.x, bottomHeadOriginalPosition.y).normalized * 15;
        }
    }
 

}
