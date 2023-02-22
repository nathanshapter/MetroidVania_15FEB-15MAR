using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaSingle : MonoBehaviour
{
  [SerializeField]  CanvasFade canvasFade;

  public  bool isTransitioning = false;
   [SerializeField] Transform other;

    [SerializeField] GameObject nextArea, nextAreaObjects;
   
    [SerializeField] GameObject prevArea;
    [SerializeField] AreaGodFather agf;
    [SerializeField] bool outputLeft, outputRight, outputTop, outputBottom;
   
    


    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isTransitioning) { return; }
            isTransitioning = true;
            StartCoroutine(canvasFade.FadeIn());
            StartCoroutine(WaitToMovePlayer());
        }
       
    }
   
    private IEnumerator WaitToMovePlayer()
    {
      AreaManager  am = GetComponentInParent<AreaManager>();
        {
            
            yield return new WaitForSeconds(.8f);


            if (isTransitioning)
            {
                other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                print(other.gameObject);
                am.movePlayer(other); // this also needs to wait }

                
                nextArea.SetActive(true);
                nextAreaObjects.SetActive(true);
                // isTransitioning= false;
                StartCoroutine(TurnOtherBackOn());
             
            }
            else { FindObjectOfType<AreaGodFather>().DisableAllAreasButFirst(); }
            isTransitioning= false;
        }
      
    }
    IEnumerator TurnOtherBackOn()
    {
        AreaManager am = GetComponentInParent<AreaManager>();
        yield return new WaitForSeconds(3);
        print("HELLO");
        
        other.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
   
   
}
