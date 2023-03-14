using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class CerberusSceneController : MonoBehaviour
{
    [Header("==========Variables To Set==========")]
    [Space(20)]
    [SerializeField] float cameraActualValue;
    [SerializeField] float fullZoomValue;
    [SerializeField] float zoomDurationIn;
    [SerializeField] float zoomDurationOut;
    [SerializeField] float cameraReturnValue;
    [Space(20)]

    [SerializeField] GameObject player;
    [SerializeField] CinemachineVirtualCamera playerCamera;
    [SerializeField] Camera cam;

    bool firstSequence = true;

    [SerializeField] BoxCollider2D playerCollider;
    [SerializeField] BridgeScript bridge;
    [SerializeField] Cerberus cerberus;
    [SerializeField] SpikeSpawner spikeSpawner;
  
    private void ZoomIn() // zooms in when falling
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
        if (cameraActualValue == cameraReturnValue) // dont need the object after camera value is in correct palce can remove if need it
            Destroy(gameObject);
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
           
            if(cameraActualValue >= 19.7) { cerberus.canStart = true; spikeSpawner.canStart = true; }
        });
        
    }
}
