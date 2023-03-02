using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
  [SerializeField]  HingeJoint2D[] hingeJoints;
   [SerializeField] GameObject[] hinge; // has to be entered manually
    public bool disableHinge;

   
    private void Start()
    {
        hingeJoints = GetComponentsInChildren<HingeJoint2D>();
      
    }

    private void Update()
    {
        if(disableHinge)
        {
            StartCoroutine(DisableAndDestroy());
        }
    }
    private IEnumerator DisableAndDestroy()
    {
        disableHinge = true;
        foreach (HingeJoint2D joint in hingeJoints)
        {
            if(joint == null) { yield break; }
            joint.enabled = false;
            yield return new WaitForSeconds(Random.Range(0.2f,0.5f));
           
            
        }
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DisableAndDestroy());
        }
    }
}
