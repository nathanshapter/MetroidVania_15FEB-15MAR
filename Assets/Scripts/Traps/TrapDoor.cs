using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{

    HingeJoint2D hinge;
    JointMotor2D hjm;
    private void Start()
    {
        hinge = GetComponent<HingeJoint2D>(); // after setting the speed, must set the motor, back to the hinge
        hjm = hinge.motor;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           hjm.motorSpeed = 0;
        }
    }
}
