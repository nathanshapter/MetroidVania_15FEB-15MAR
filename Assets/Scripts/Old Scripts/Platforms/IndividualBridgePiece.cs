using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualBridgePiece : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Wall"))
        {
            StartCoroutine(DestroyPiece());
        }
    }
    private IEnumerator DestroyPiece()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
