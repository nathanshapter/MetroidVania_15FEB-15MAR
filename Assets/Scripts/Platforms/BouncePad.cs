using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] float bounce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")  )
            {
            collision.gameObject.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            }
    }
}
