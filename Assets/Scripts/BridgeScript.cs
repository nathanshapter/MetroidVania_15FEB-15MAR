using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
  [SerializeField]  HingeJoint2D[] hingeJoints;


    private void Start()
    {
        hingeJoints = GetComponentsInChildren<HingeJoint2D>();
    }
}
