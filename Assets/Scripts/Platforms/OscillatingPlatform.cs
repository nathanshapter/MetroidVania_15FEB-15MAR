using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatingPlatform : MonoBehaviour
{

    [SerializeField] [Range(0,Mathf.PI*2)] float movementDelayInRadians;


    [Header("Straight Movement")]
    Vector2 startingPosition;
    [SerializeField] Vector2 movementVector;
    [SerializeField][Range(0, 1)] float movementFactor;
    [SerializeField] float period = 2f;



    [Header("Circle Movement")]
    [SerializeField] bool isCircle;
    [SerializeField] bool lookAtCentre;
    [SerializeField] bool mirrorMovement;
    [SerializeField] bool clockWise;
    [SerializeField] Transform rotationCentre;
    [SerializeField] float rotationRadius = 2f, angularSpeed = 2f;
    float posX, posY, angle = 0f;
    [SerializeField] float rotationOffset;


  

    private void Start()
    {
        startingPosition= transform.position;
    }
    private void Update()
    {
        if (!isCircle) 
        {
            float cycles = Time.time / period;

            const float tau = Mathf.PI * 2;
            float rawSinWave = Mathf.Sin(cycles * tau);

            movementFactor = (rawSinWave + 1f) / 2;

            Vector2 offset = movementVector * movementFactor;
            transform.position = startingPosition + offset;
        }
        else
        {

            if(mirrorMovement && !clockWise)
            {
                posX = rotationCentre.position.x + Mathf.Cos(angle + movementDelayInRadians) * -rotationRadius ;
                posY = rotationCentre.position.y + Mathf.Sin(angle + movementDelayInRadians) * -rotationRadius  ;
            }            
            else if(mirrorMovement && clockWise)
            {
                posX = rotationCentre.position.x + Mathf.Cos(-angle + movementDelayInRadians) * -rotationRadius;
                posY = rotationCentre.position.y + Mathf.Sin(-angle + movementDelayInRadians) * -rotationRadius ;
            }
            else 
            {
                posX = rotationCentre.position.x + Mathf.Cos(angle + movementDelayInRadians) * rotationRadius ;
                posY = rotationCentre.position.y + Mathf.Sin(angle + movementDelayInRadians) * rotationRadius ;



            }

            transform.position = new Vector2(posX, posY);
            angle = angle + Time.deltaTime * angularSpeed ;
            if (angle >= 360f)
                angle = 0f;
        }
        if(lookAtCentre )
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
}   
