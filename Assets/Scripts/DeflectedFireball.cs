using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectedFireball : MonoBehaviour
{
    [Header("============Variables To Set ==========")]
    [SerializeField] float moveSpeed;
    [HideInInspector] public EnemyHealth target; // to target enemy

    Rigidbody2D rb;
    Vector2 moveDirection;
    [SerializeField] private AudioClip fireBall;

    private void Start()
    {
        // add sound to fireball
        target = FindObjectOfType<EnemyHealth>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        moveDirection = (new Vector3(target.transform.position.x, target.transform.position.y, 0) - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        this.transform.SetParent(null);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("collided");
    }
}
