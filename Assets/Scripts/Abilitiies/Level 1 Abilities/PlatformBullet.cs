using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformBullet : MonoBehaviour 
{
    [SerializeField] public GameObject bullet, wallFloor;
    bool bulletPlatformJustSpawned;
    PlayerMovement playerMovement;
   

    
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float timeBetweenBullets;
    [SerializeField] Animator anim;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
       
    }

    public void FirePlatform(InputAction.CallbackContext context)
    {
        if (playerMovement.IsGrounded())
        {
            playerMovement.DisableMovement(0.6f);
        }

        if (!ProgressionManager.instance.progression[3]) { return; }
        if (!bulletPlatformJustSpawned && !playerMovement.isCrouching)
        {
            StartCoroutine(ResetPlatformBullet());

        }
    }
    IEnumerator ResetPlatformBullet() // this also shoots it with a timer to line up with the animation
    {
        anim.SetTrigger("Throw");
        bulletPlatformJustSpawned = true;
        yield return new WaitForSeconds(0.25f);
        Instantiate(bullet, bulletSpawn.position, transform.rotation);


        yield return new WaitForSeconds(timeBetweenBullets);
        bulletPlatformJustSpawned = false;
    }
}
