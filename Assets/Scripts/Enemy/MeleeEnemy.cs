using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
   [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] float range;
    [SerializeField] float colliderDistance;
    private float cooldownTimer = Mathf.Infinity;
   [SerializeField] private BoxCollider2D boxCollider;
    EnemyHealth eh;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Health playerHealth;// gets player health script
    private Animator anim;

    private EnemyPatrol enemyPatrol;
    private void Awake()
    {
        playerHealth = FindObjectOfType<Health>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        anim = GetComponent<Animator>();
        eh = GetComponent<EnemyHealth>();
        damage = eh.attackdamage;
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            Attack();
        }
        if(enemyPatrol!= null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private void Attack()
    {
        if (cooldownTimer > attackCooldown)
        {
            cooldownTimer = 0;
            anim.SetTrigger("meleeAttack");
            
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range* transform.localScale.x * colliderDistance,
            new Vector3( boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z ),
            0, Vector2.left, 0, playerLayer);
       

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance
            , new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);

        }
    }

}
