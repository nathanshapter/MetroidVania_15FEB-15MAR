using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cerberus : MonoBehaviour
{
    [SerializeField] public GameObject[] heads;
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

    private int amountOfDropsInWave; // to implement later if necessary, time bewtween waves at the moment they are endless

    [SerializeField] CircleCollider2D bottomHeadCircleCollider;

    [SerializeField] CerberusFireProjectile projectile;

    [SerializeField] LayerMask playerLayer;

    float distanceBetweenPlayer;

    [SerializeField] Health target;

    [SerializeField] Rigidbody2D bottomHeadrb;

    Vector2 moveDirection;

    [SerializeField] Vector2 bottomHeadOriginalPosition;





    private void Start()
    {
        canBite = true;
        target = FindObjectOfType<Health>();
        timeBetweenWaveSpawns = cerberusStage[0].timeBetweenWaveSpawns;
        timeBetweenFireballSpawn = cerberusStage[0].timeBetweenFireballSpawn;
        amountOfBallsToSpawn = cerberusStage[0].amountOfBallsToSpawn;
        amountOfDropsInWave = cerberusStage[0].amountOfDropsInWave;

        StartCoroutine(SpawnFireballs());


     bottomHeadOriginalPosition = heads[2].transform.position;
    }
    public void GetStageValues()
    {
        timeBetweenWaveSpawns = returnStage().timeBetweenWaveSpawns;
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
        timeBetweenLastWave = 0;
        waveInProgress = false;

    }


    private void Update()
    {
        if (heads[2].transform.position.x < 12)
        {
            heads[2].transform.position = bottomHeadOriginalPosition;
        }
       
        health = GetComponent<EnemyHealth>().health;
        

        timeBetweenLastWave += Time.deltaTime;
        if (timeBetweenLastWave > timeBetweenWaveSpawns && !waveInProgress)
        {
            
            StartCoroutine(SpawnFireballs());
            selfHit = 0;
        }

        GetStageValues();

        distanceBetweenPlayer = Vector3.Distance(target.transform.position, heads[2].transform.position);

        if(distanceBetweenPlayer >= 20) {  }
        else
        {
            if (isBiting) { return; }
            else if(canBite) { StartCoroutine(Bite()); }
            
        }
        

       
        if(isBiting)
        {
            print("isBiting");
            bottomHeadrb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        }
        else
        {
            print("is returning");
            bottomHeadrb.velocity = new Vector2(bottomHeadOriginalPosition.x, bottomHeadOriginalPosition.y).normalized; // I DONT GET WHY ITS NOT WORKING 
            heads[2].transform.position = bottomHeadOriginalPosition;
        //    print(bottomHeadOriginalPosition);
          //  bottomHeadrb.velocity = new Vector2(bottomHeadOriginalPosition.x -2, bottomHeadOriginalPosition.y +1);    
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

   public bool isBiting;
    private bool canBite;
  

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



        yield return new WaitForSeconds(10);
        canBite = true;
        print(canBite);

    }
   

  
}
