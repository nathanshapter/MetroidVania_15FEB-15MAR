using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [Header("==========Platform Variables==========")]
    [Space(20)]
    [SerializeField] float timeBeforeSelfDestruct = 10f;
    
    void Start()
    {
        StartCoroutine(DestroyPlatform());
    }

   IEnumerator DestroyPlatform()
    { // this will crumble rather than just destroy
        yield return new WaitForSeconds(timeBeforeSelfDestruct);
        Destroy(gameObject);
    }
}
