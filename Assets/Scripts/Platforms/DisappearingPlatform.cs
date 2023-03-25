using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] int timeUntilDestroy =3;
    [SerializeField] int timeUntilRespawn = 3;
    BoxCollider2D boxCollider2D;
    SpriteRenderer sprite;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PlatformEnableDisable());
        }
    }
    IEnumerator PlatformEnableDisable()
    {
       
        yield return new WaitForSeconds(timeUntilDestroy);
        playerMovement.transform.SetParent(null);
        sprite.enabled = false; // instead of being false, future update will have it play an animation, and turn into nothing
        boxCollider2D.enabled = false;
       
        yield return new WaitForSeconds(timeUntilRespawn);
        this.gameObject.SetActive(true);
        sprite.enabled = true;
        boxCollider2D.enabled = true;
       
    }
   
}
