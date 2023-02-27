using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerberusFireProjectile : MonoBehaviour
{
  [SerializeField]  float moveSpeed;
    [SerializeField] Health target; // to target player
    Rigidbody2D rb;
    Vector2 moveDirection;

  [SerializeField]  Cerberus cerberus;
    

    [SerializeField] float angleVarianceClose;
    [SerializeField] float angleVarianceFar;

    float distanceBetweenPlayer;
    private void Start()
    {
        
        target = FindObjectOfType<Health>();
        rb= GetComponent<Rigidbody2D>();
        cerberus = FindObjectOfType<Cerberus>();
        distanceBetweenPlayer = Vector3.Distance(target.transform.position, cerberus.heads[2].transform.position);
        if (distanceBetweenPlayer >= 20)
        {
            moveDirection = (new Vector3(target.transform.position.x * Random.Range(1, angleVarianceFar), target.transform.position.y * Random.Range(1, angleVarianceFar)) - transform.position).normalized * moveSpeed;
        }
        else
        {
            moveDirection = (new Vector3(target.transform.position.x * Random.Range(1, angleVarianceClose), target.transform.position.y * Random.Range(1, angleVarianceClose)) - transform.position).normalized * moveSpeed;
        }
       
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        
        // how close is he to bottom head
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
    private void Update()
    {
        print(distanceBetweenPlayer);
    }

}
