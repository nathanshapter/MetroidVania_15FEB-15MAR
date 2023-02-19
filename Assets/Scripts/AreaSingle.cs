using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaSingle : MonoBehaviour
{
  [SerializeField]  CanvasFade canvasFade;

  public  bool isTransitioning = false;
   [SerializeField] Transform other;

    [SerializeField] GameObject nextArea;
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        
        StartCoroutine(canvasFade.FadeIn());
        StartCoroutine(WaitToMovePlayer());
    }
    private IEnumerator WaitToMovePlayer()
    {
      AreaManager  am = GetComponentInParent<AreaManager>();
        {
            isTransitioning = true;
            yield return new WaitForSeconds(.8f);


            if (isTransitioning)
            {
                am.movePlayer(other); // this also needs to wait }
                other.gameObject.SetActive(false); // and then come back
                FindObjectOfType<AreaGodFather>().DisableAllAreas();
                nextArea.SetActive(true);
                // isTransitioning= false;
                StartCoroutine(TurnOtherBackOn());

            }                 

        }
      
    }
    IEnumerator TurnOtherBackOn()
    {
        yield return new WaitForSeconds(1);
        print("HELLO");
        other.gameObject.SetActive(true);
    }

}
