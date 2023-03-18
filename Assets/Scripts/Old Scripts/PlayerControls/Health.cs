using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("==========Health Values==========")]
    [Space(20)]
    public int playerHealth;
    public int amountOfLives = 5;
    [Space(20)]

    [Header("==========Damage Values==========")]
    [Space(20)]
    [SerializeField] int spikeDamage;
    [SerializeField] float invincibleTimer = 1f, invincibleTimerOriginal =1f;
    public bool fallDamage = false;
    public bool justTookDamage;
    public bool canTakeDmg = true;
    [SerializeField] float justTookDamageTime;

    [Space(20)]
    [Header("==========Scripts==========")]
    [Space(20)]

    [SerializeField] GameObject playerPrefab;
    [SerializeField] DeathManager deathManager;
    [SerializeField] RespawnManager respawnManager;
    [SerializeField] PlayerShader playerShader;
    [SerializeField] CheckpointsManager cpManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
             fallDamage = true;
            TakeDamage(spikeDamage);
            deathManager.fallRespawn = true;
            if (!CheckIfAlive()) { deathManager.ProcessDeath();  } // add death screen            
        }
    }

    public static Health instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
    private void Update()
    {
        invincibleTimer -= Time.deltaTime;
    }
    public int TakeDamage(int damage)
    {
        if (canTakeDmg)
        {
            // SoundManager.Instance.StopSound(); // needs to stop just flute
            justTookDamage = true;
            if (invincibleTimer > 0) { return playerHealth; }
            playerShader.PlayShaderDamage();
            playerHealth -= damage;
            print(damage);

            invincibleTimer = invincibleTimerOriginal;
            CheckIfAlive();
            StartCoroutine(resetDamageBool());
        }
      
        return playerHealth;
    }
    public bool CheckIfAlive()
    {        
        if(playerHealth <= 0) { respawnManager.RespawnPlayer();cpManager.lastCheckPointPos = null;  return false; } // temp solution , need to stop it from being called again

        if (fallDamage && playerHealth < amountOfLives){ StartCoroutine(deathManager.InitiateRespawn()); fallDamage = false;  return true; }    
        else { fallDamage = false; return true; }         

    }
    private IEnumerator resetDamageBool()
    {
        yield return new WaitForSeconds(justTookDamageTime);
        justTookDamage= false;
    }
}
