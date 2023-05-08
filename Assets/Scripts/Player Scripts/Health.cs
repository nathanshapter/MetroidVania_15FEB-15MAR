using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    public bool isDead;

    [Space(20)]
    [Header("==========Scripts==========")]
    [Space(20)]

    [SerializeField] GameObject playerPrefab;
    [SerializeField] DeathManager deathManager;
    [SerializeField] RespawnManager respawnManager;
    [SerializeField] PlayerShader playerShader;
    [SerializeField] CheckpointsManager cpManager;
    [SerializeField] Animator animator;
    [SerializeField] PlayerMovement player;

    [SerializeField] float knockbackForce = 4f; // to be added into enemy values later
    Rigidbody2D rb;

    CheckpointsManager cp;

    private void Start()
    {
        cp = FindObjectOfType<CheckpointsManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
             fallDamage = true;
            TakeDamage(spikeDamage,0,0);
            deathManager.fallRespawn = true;
            if (!CheckIfAlive()) { deathManager.ProcessDeath(); } else
            {
               deathManager.RespawnPlayer(cp.lastCheckPointPos); // this needs to be changed to work with last palce
            }
        }
    }

    public static Health instance;
    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();    
        animator = GetComponentInChildren<Animator>();
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
        animator.SetBool("isDead", isDead);
    }
  public bool canBeknocked = false;
    public int TakeDamage(int damage, float knockbackX, float knockbackY) // needs to be negative or positive depending on enemies positions
    {
        if (canTakeDmg && !isDead)
        {
            transform.SetParent(null);
            print(damage);
           canBeknocked= true;
            // SoundManager.Instance.StopSound(); // needs to stop just flute
            justTookDamage = true;
            if (invincibleTimer > 0) { return playerHealth; }
            animator.SetTrigger("Hurt");
            playerShader.PlayShaderDamage();
            playerHealth -= damage;
           
            invincibleTimer = invincibleTimerOriginal;
            CheckIfAlive();

            rb.AddForce(new Vector2(knockbackX, knockbackY), ForceMode2D.Impulse);
            
           
            StartCoroutine(resetDamageBool());
        }
      
        return playerHealth;
    }
    public bool CheckIfAlive()
    {        
        if(playerHealth <= 0) { respawnManager.RespawnPlayer(); cpManager.lastCheckPointPos = null; StartCoroutine(Die()); return false; } // temp solution , need to stop it from being called again

        if (fallDamage && playerHealth < amountOfLives){ StartCoroutine(deathManager.InitiateRespawn()); fallDamage = false;  return true; }    
        else { fallDamage = false; return true; }         

    }
    private IEnumerator resetDamageBool()
    {
        yield return new WaitForSeconds(0.15f);
        canBeknocked= false;

        yield return new WaitForSeconds(justTookDamageTime);
        justTookDamage= false;
    }
    private IEnumerator Die()
    {
        isDead= true;
        animator.SetTrigger("Death");
        deathManager.totalDeaths++;
        yield return new WaitForSeconds(3);
        
        isDead = false;

    }
   
}
