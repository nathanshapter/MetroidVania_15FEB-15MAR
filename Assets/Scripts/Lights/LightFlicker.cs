using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] Light2D iceLight;
    [SerializeField] float period = 2f;
    [SerializeField] float addedIntensity;
    [SerializeField] bool turnOnProximity = false;
    PlayerMovement player;
    [SerializeField] int turnOnDistance = 5;


    private void Start()
    {
        player= FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {

        if (turnOnProximity)
        {
            if(Vector2.Distance(player.transform.position, iceLight.transform.position)< turnOnDistance)
            {
                turnOnProximity= false;
            }

            iceLight.intensity = 0;
            return;
        }

        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau +1);

        iceLight.intensity = (rawSinWave + 1) +addedIntensity;


       
      
    }
}
