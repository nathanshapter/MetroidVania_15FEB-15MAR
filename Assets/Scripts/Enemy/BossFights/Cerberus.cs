using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cerberus : MonoBehaviour
{

    [Header("==========AllHeads Logic==========")]
    [Space(20)]
    //gets
    [SerializeField] public GameObject[] heads;    
    [SerializeField] HeadScript[] headScript;
    public CerberusStages[] cerberusStage;
    [SerializeField] SpriteRenderer[] headSprites;

    [SerializeField] Transform playerTransform;
    [SerializeField] Health target; // this is to locate the player

   [HideInInspector] public bool isSleeping = false;   
    
    [SerializeField] Sprite spriteOpenMouth, spriteClosedMouth, spriteAsleep;
    

    [SerializeField] private AudioClip growl;

    [HideInInspector] public bool canStart = false;

    [Header("Variables To Set")]
    [SerializeField] float timeUntilFluteSleep;
    [SerializeField] float lengthOfFluteSleep;
    private int health;
    [SerializeField] int healthTo2ndStage, healthTo3rdStage, healthTo4thStage, healthToSecretStage;

    [Header("===========TopHead Logic==========")]
    [Space(20)]
    [Header("==========MiddleHead Logic==========")]
    [Space(20)]
    // gets
    [SerializeField] GameObject fireball;
    [SerializeField] Transform fireballSpawn;
    [SerializeField] CerberusFireProjectile projectile;

   
    
     int amountOfBallsToSpawn = 5;

    // variables
    [SerializeField] float timeBetweenLastFireballWave;
     bool waveInProgress = false;

    float timeBetweenFireBallWaveSpawns = 5;
    float timeBetweenFireballSpawn = 0.5f;    
  [HideInInspector]  public int fireballHits;    // how many times the player is hit
    int amountOfBallsSpawned = 0;
   [HideInInspector] public int selfHit = 0; // how many times cerberus hits itself
    int amountOfDropsInWave;

    [Header("==========BottomHead Logic==========")]
    [Space(20)]
    // gets
    [SerializeField] Rigidbody2D bottomHeadrb;
    [SerializeField] CircleCollider2D bottomHeadCircleCollider;
    [SerializeField] Vector2 bottomHeadOriginalPosition;
    [Header("Variables To Set")]
    [SerializeField] float distanceBeforeAttack = 12;
    [SerializeField] float timeInBetweenBites;

    // sets
    Vector2 moveDirection;
    // variables
    float distanceBetweenPlayer;
    [HideInInspector] public bool isBiting;
    private bool canBite = true;
    





    private void Start()
    {
        foreach (SpriteRenderer i in headSprites)
        {
            i.sprite = spriteAsleep;
        }
        canBite = true;
        target = FindObjectOfType<Health>();
        timeBetweenFireBallWaveSpawns = cerberusStage[0].timeBetweenWaveSpawns;
        timeBetweenFireballSpawn = cerberusStage[0].timeBetweenFireballSpawn;
        amountOfBallsToSpawn = cerberusStage[0].amountOfBallsToSpawn;
        amountOfDropsInWave = cerberusStage[0].amountOfDropsInWave;    


     bottomHeadOriginalPosition = heads[2].transform.position.normalized;

      
    }
    public void GetStageValues()
    {
        timeBetweenFireBallWaveSpawns = returnStage().timeBetweenWaveSpawns;
        timeBetweenFireballSpawn = returnStage().timeBetweenFireballSpawn;
        amountOfBallsToSpawn = returnStage().amountOfBallsToSpawn;
        amountOfDropsInWave = returnStage().amountOfDropsInWave;

    }
  
    IEnumerator SpawnFireballs() // middle head logic to spawn fire
    {
        waveInProgress = true;
        headSprites[1].sprite = spriteOpenMouth;
        if (amountOfBallsSpawned < amountOfBallsToSpawn)
        {
            for (amountOfBallsSpawned = 0; amountOfBallsSpawned < amountOfBallsToSpawn; amountOfBallsSpawned++)
            {
                if (isSleeping == true) { waveInProgress = false; headSprites[1].sprite = spriteAsleep; yield break; }
                if (selfHit <= 5)
                {
                    yield return new WaitForSeconds(timeBetweenFireballSpawn);
                    Instantiate(fireball, fireballSpawn.position, fireballSpawn.transform.rotation);
                    
                }

            }
        }
        amountOfBallsSpawned = 0;
        timeBetweenLastFireballWave = 0;
        headSprites[1].sprite = spriteClosedMouth;
        waveInProgress = false;
    }
    bool activated = false;
    void ActivateDogs() // called one time to open their eyes and start shooting fire right away
    {

       

        foreach (SpriteRenderer i in headSprites)
            {
                i.sprite = spriteClosedMouth;
            }
            activated= true;
        StartCoroutine(SpawnFireballs());
    }
    private void Update()
    {
        if(!canStart) return;
        if (!activated) { ActivateDogs(); }
        if (!isSleeping)
        {
            if (heads[2].transform.position.x <= 18 || heads[2].transform.position.y >= 2)
            {
                canBite = false;

            }
            else
            {
                canBite = true;


            }

            health = GetComponent<EnemyHealth>().health; // to locate player
            timeBetweenLastFireballWave += Time.deltaTime;
            BeginNewFireballWave();
            GetStageValues();

            distanceBetweenPlayer = Vector3.Distance(target.transform.position, transform.position); // update distance in real time

            ProcessBite();

            foreach(HeadScript h in headScript)
            {
                h.LookAtPlayer();
            }

        }
      
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
        if(health <= healthTo4thStage)
        {
            return cerberusStage[3];
        }
        if(health<= healthTo3rdStage)
        {
            return cerberusStage[2];
        }
        if(health <= healthTo2ndStage)
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
        headSprites[2].sprite = spriteOpenMouth;
        float biteSpeed = 100;
        Vector2 targetPosition = target.transform.position;
        moveDirection = (new Vector3(targetPosition.x, targetPosition.y) - heads[2].transform.position).normalized * biteSpeed;
       
        isBiting = true;
         // bite the player
        
        yield return new WaitForSeconds(1);
        
        // return head to original position
        
        isBiting = false;



        yield return new WaitForSeconds(timeInBetweenBites);
        headSprites[2].sprite = spriteClosedMouth;
        canBite = true;
       

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
            
            bottomHeadrb.velocity = new Vector2(moveDirection.x, moveDirection.y).normalized * 50;
        }
        else
        {
            
            bottomHeadrb.velocity = new Vector2(bottomHeadOriginalPosition.x, bottomHeadOriginalPosition.y).normalized * 15;
        }
        
    }
 
    public IEnumerator SetSleepCerberus()
    {
        Health playerHealth = FindObjectOfType<Health>();
        if (playerHealth.justTookDamage) {  yield break; }
        yield return new WaitForSeconds(timeUntilFluteSleep); // if player took damage, return
        if (playerHealth.justTookDamage) {  yield break; }
        isSleeping= true;       
        
       foreach (SpriteRenderer i in headSprites)
            {
                i.sprite = spriteAsleep;
            }
        

        yield return new WaitForSeconds(lengthOfFluteSleep -1);
        SoundManager.Instance.PlaySound(growl);
        yield return new WaitForSeconds(1);
        isSleeping = false;
        foreach (SpriteRenderer i in headSprites)
        {
            i.sprite = spriteClosedMouth;
        }

    }
}
