using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformBullet : MonoBehaviour 
{
    [SerializeField] public GameObject bullet, wallFloor;
    bool bulletPlatformJustSpawned;

    ProgressionManager progressionManager;

    
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float timeBetweenBullets;

    private void Start()
    {
        progressionManager = FindObjectOfType<ProgressionManager>();
    }

    public void FirePlatform(InputAction.CallbackContext context)
    {
        print("hello");
        if (!progressionManager.progression[3]) { return; }
        if (!bulletPlatformJustSpawned)
        {
            Instantiate(bullet, bulletSpawn.position, transform.rotation);
            bulletPlatformJustSpawned = true;
            StartCoroutine(ResetPlatformBullet());

        }
    }
    IEnumerator ResetPlatformBullet()
    {
        yield return new WaitForSeconds(timeBetweenBullets);
        bulletPlatformJustSpawned = false;
    }
}
