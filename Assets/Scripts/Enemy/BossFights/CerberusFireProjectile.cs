using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerberusFireProjectile : MonoBehaviour
{
    [Header("==========Variables To Set==========")]
    [Space(20)]
    [SerializeField] float moveSpeed;
    [SerializeField] float angleVarianceClose;
    [SerializeField] float angleVarianceFar;
    [SerializeField] private AudioClip fireBall;

    // variables to get
    Health target; // to target player
    Rigidbody2D rb;
    Vector2 moveDirection;
    Cerberus cerberus;
    float distanceBetweenPlayer;





   
    private void Start()
    {
        target = FindObjectOfType<Health>(); // later on this needs to be found in the parent ofbject so it is not just running off findobject of type
        rb = GetComponent<Rigidbody2D>();
        cerberus = FindObjectOfType<Cerberus>();

        SoundManager.Instance.PlaySound(fireBall);

        CalculateAngleVariance();

        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
       
    }

    private void CalculateAngleVariance()
    {
        distanceBetweenPlayer = Vector3.Distance(target.transform.position, cerberus.heads[2].transform.position);
        if (distanceBetweenPlayer >= 20)
        {

            moveDirection = (new Vector3(target.transform.position.x * Random.Range(1, angleVarianceFar), target.transform.position.y * Random.Range(1, angleVarianceFar)) - transform.position).normalized * moveSpeed;
        }
        else
        {
            moveDirection = (new Vector3(target.transform.position.x * Random.Range(1, angleVarianceClose), target.transform.position.y * Random.Range(1, angleVarianceClose)) - transform.position).normalized * moveSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // player detects the damage, do not need to here
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cerberus.fireballHits++;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            cerberus.selfHit++;
        }

        Destroy(gameObject);
    }
    

}
