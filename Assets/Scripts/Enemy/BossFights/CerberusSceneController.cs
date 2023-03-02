using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class CerberusSceneController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] CinemachineVirtualCamera playerCamera;
    [SerializeField] Camera cam;
  [SerializeField]  BridgeScript bridge;
    bool firstSequence = true;
    [SerializeField] BoxCollider2D playerCollider;

    [SerializeField] float cameraActualValue, fullZoomValue, zoomDurationIn, zoomDurationOut, cameraReturnValue;



    [SerializeField] Cerberus cerberus;
    [SerializeField] SpikeSpawner spikeSpawner;
    private void Start()
    {
      

    }

    private void ZoomOut()
    {
       
    }
    private void ZoomIn()
    {
        DOTween.To(() => cameraActualValue, x => cameraActualValue = x, fullZoomValue, zoomDurationIn);
    }

    private void Update()
    {
        playerCamera.m_Lens.OrthographicSize = cameraActualValue;

        if (bridge.disableHinge)
        {
            
            if (firstSequence == true)
            {
                StartCoroutine(StartFall());
                firstSequence = false;
            }
            
        }
        if(player.transform.position.y < 15)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<Rigidbody2D>().constraints =  RigidbodyConstraints2D.FreezeRotation;
            playerCollider.enabled = true;
           
            
        }
    }

    private IEnumerator StartFall()
    {
        ZoomIn();
        player.GetComponent<PlayerMovement>().enabled = false;


        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(3);
        playerCollider.enabled = false;
        yield return new WaitForSeconds(4);
        print("hello");
        DOTween.To(() => cameraActualValue, x => cameraActualValue = x, cameraReturnValue, zoomDurationOut).SetEase(Ease.InOutSine).OnUpdate(() =>
        {
            Debug.Log(cameraActualValue);
            if(cameraActualValue >= 19.7) { cerberus.canStart = true; spikeSpawner.canStart = true; }
        });
        
    }
}
