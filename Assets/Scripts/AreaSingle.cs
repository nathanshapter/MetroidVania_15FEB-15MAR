using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSingle : MonoBehaviour
{
  [SerializeField]  CanvasFade canvasFade;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        print(gameObject);
        StartCoroutine(canvasFade.FadeIn());
        StartCoroutine(WaitToMovePlayer());
    }
    private IEnumerator WaitToMovePlayer()
    {
        yield return new WaitForSeconds(.8f);
        GetComponentInParent<AreaManager>().movePlayer(); // this also needs to wait
    }
}
