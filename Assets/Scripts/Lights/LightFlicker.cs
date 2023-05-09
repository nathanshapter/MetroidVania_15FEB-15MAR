using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] Light2D iceLight;
    [SerializeField] float period = 2f;
    [SerializeField] float addedIntensity;
    private void Update()
    {
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau +1);

        iceLight.intensity = (rawSinWave + 1) +addedIntensity;
      
    }
}
