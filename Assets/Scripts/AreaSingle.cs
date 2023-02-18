using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaSingle : MonoBehaviour
{
  [SerializeField]  CanvasFade canvasFade;

  public  bool isTransitioning = false;
   [SerializeField] Transform other;

    
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        print(gameObject);
        StartCoroutine(canvasFade.FadeIn());
        StartCoroutine(WaitToMovePlayer());
    }
    private IEnumerator WaitToMovePlayer()
    {
       
        {
            isTransitioning = true;
            yield return new WaitForSeconds(.8f);


            if (isTransitioning)
            {
                GetComponentInParent<AreaManager>().movePlayer(other); // this also needs to wait }
                other.gameObject.SetActive(false); // and then come back
                                                   // isTransitioning= false;
                yield return new WaitForSeconds(3);
                other.gameObject.SetActive(true);
            }                 

        }
      
    }

}
