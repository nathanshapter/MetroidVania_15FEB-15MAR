using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturningPlatform: MonoBehaviour
{

    


    [Header("Straight Movement")]
    Vector2 startingPosition;
    [SerializeField] Vector2 movementVector;
    [SerializeField][Range(0, 1)] float movementFactor;
    [SerializeField] float period = 2f;



   

  

    private void Start()
    {
        startingPosition= transform.position;
    }
    private void Update()
    {
        
        
            float cycles = Time.time / period;

            const float tau = Mathf.PI * 2;
            float rawSinWave = Mathf.Sin(cycles * tau);

            movementFactor = (rawSinWave + 1f) / 2;

            Vector2 offset = movementVector * movementFactor;
            transform.position = startingPosition + offset;
        
    
       
       
    }
  
}   
