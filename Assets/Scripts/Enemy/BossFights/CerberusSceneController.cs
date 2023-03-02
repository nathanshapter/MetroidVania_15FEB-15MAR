using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CerberusSceneController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] CinemachineVirtualCamera playerCamera;
  [SerializeField]  BridgeScript bridge;
    bool firstSequence = true;
    [SerializeField] BoxCollider2D playerCollider;
    private void Update()
    {
        if(bridge.disableHinge)
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
        player.GetComponent<PlayerMovement>().enabled= false;
        playerCamera.m_Lens.OrthographicSize = 3;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(3);
        playerCollider.enabled = false;

    }
}
