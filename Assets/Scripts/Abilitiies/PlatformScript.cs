using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [SerializeField] float timeBeforeDestroy = 10f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyPlatform());
    }

   IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(timeBeforeDestroy);
        Destroy(gameObject);
    }
}
