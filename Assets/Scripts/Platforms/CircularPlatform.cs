using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPlatform : MonoBehaviour
{
    [Header("Circle Movement")]
    [Space(20)]
    [SerializeField] bool lookAtCentre;
    [SerializeField] bool mirrorMovement;
    [SerializeField] bool clockWise;
    [SerializeField] Transform rotationCentre;
    [Space(20)]
    [Header("Rotation Values")]
    [Space(20)]
    [SerializeField] float rotationRadius = 2f;
    [Tooltip("The amount of seconds it takes to complete a rotation.")]
    [SerializeField] float    angularSpeed = 2f;
    float posX, posY, angle = 0f;
    [Tooltip("This can correct the rotation angle if it is looking at something incorrectly.")]
    [SerializeField] float rotationOffset;
    [Tooltip("The range is 0 - 2Pi. To correctly delay something at halfway you would put it as 6.28 * 0.5, or 3.14. Same to find %75, or %25 of the way.")]
    [SerializeField][Range(0, Mathf.PI * 2)] float movementDelayInRadians;
  
    private void Update()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        if (mirrorMovement && !clockWise)
        {
            posX = rotationCentre.position.x + Mathf.Cos(angle + movementDelayInRadians) * -rotationRadius;
            posY = rotationCentre.position.y + Mathf.Sin(angle + movementDelayInRadians) * -rotationRadius;
        }
        else if (mirrorMovement && clockWise)
        {
            posX = rotationCentre.position.x + Mathf.Cos(-angle + movementDelayInRadians) * -rotationRadius;
            posY = rotationCentre.position.y + Mathf.Sin(-angle + movementDelayInRadians) * -rotationRadius;
        }
        else
        {
            posX = rotationCentre.position.x + Mathf.Cos(angle + movementDelayInRadians) * rotationRadius;
            posY = rotationCentre.position.y + Mathf.Sin(angle + movementDelayInRadians) * rotationRadius;

        }
        transform.position = new Vector2(posX, posY);
        angle = angle + Time.deltaTime * angularSpeed;
        if (angle >= 360f)
            angle = 0f;

        if (lookAtCentre)
        {
            LookAtCentre();
        }
    }

    public void LookAtCentre()
    {
        var dir = rotationCentre.position - this.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + rotationOffset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rotationCentre.transform.position, rotationRadius);
    }
}
