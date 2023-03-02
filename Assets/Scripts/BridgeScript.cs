using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
  [SerializeField]  HingeJoint2D[] hingeJoints;
   [SerializeField] GameObject[] hinge; // has to be entered manually
    [SerializeField] bool disableHinge;

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
        foreach (HingeJoint2D joint in hingeJoints)
        {
            joint.enabled = false;
            yield return new WaitForSeconds(Random.Range(0.2f,0.5f));
           
            
        }
      
    }
}
